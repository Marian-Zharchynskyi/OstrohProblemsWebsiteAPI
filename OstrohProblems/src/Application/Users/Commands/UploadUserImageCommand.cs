using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Services;
using Application.Services.ImageService;
using Application.Services.TokenService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Optional;

namespace Application.Users.Commands;

public record UploadUserImageCommand : IRequest<Result<JwtVm, UserException>>
{
    public Guid UserId { get; init; }
    public IFormFile ImageFile { get; init; }
}

public class UploadUserImageCommandHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    IImageService imageService)
    : IRequestHandler<UploadUserImageCommand, Result<JwtVm, UserException>>
{
    public async Task<Result<JwtVm, UserException>> Handle(UploadUserImageCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match<Task<Result<JwtVm, UserException>>>(
            async user => await UploadOrReplaceImage(user, request.ImageFile, cancellationToken),
            () => Task.FromResult<Result<JwtVm, UserException>>(
                new UserNotFoundException(userId)));
    }

    private async Task<Result<JwtVm, UserException>> UploadOrReplaceImage(
        User user,
        IFormFile imageFile,
        CancellationToken cancellationToken)
    {
        var imageSaveResult = await imageService.SaveImageFromFileAsync(ImagePaths.UserImagePath, imageFile, user.UserImage?.FilePath);

        return await imageSaveResult.Match<Task<Result<JwtVm, UserException>>>(
            async imageName =>
            {
                var imageEntity = UserImage.New(UserImageId.New(), user.Id, imageName);
                user.UpdateUserImage(imageEntity);
                var userWithNewImage = await userRepository.Update(user, cancellationToken);
                return await jwtTokenService.GenerateTokensAsync(userWithNewImage, cancellationToken);
            },
            () => Task.FromResult<Result<JwtVm, UserException>>(new ImageSaveException(user.Id)));
    }
}
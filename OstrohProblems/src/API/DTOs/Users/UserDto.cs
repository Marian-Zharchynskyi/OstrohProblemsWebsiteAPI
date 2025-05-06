using API.DTOs;
using API.DTOs.Problems;
using Domain.Identity.Users;

namespace Api.Dtos.Users;

public record UserDto(
    Guid? Id,
    string Email,
    string? Name,
    UserImageDto? Image,
    List<RoleDto>? Roles,
    List<ProblemDto>? Problems,
    List<CommentDto>? Comments)
{
    public static UserDto FromDomainModel(User user)
        => new(
            user.Id.Value,
            user.Email,
            user.FullName,
            user.UserImage != null ? UserImageDto.FromDomainModel(user.UserImage) : null,
            user.Roles.Count == 0 ? null : user.Roles.Select(RoleDto.FromDomainModel).ToList(),
            user.Problems.Count == 0 ? null : user.Problems.Select(ProblemDto.FromDomainModel).ToList(),
            user.Comments.Count == 0 ? null : user.Comments.Select(CommentDto.FromDomainModel).ToList());
}
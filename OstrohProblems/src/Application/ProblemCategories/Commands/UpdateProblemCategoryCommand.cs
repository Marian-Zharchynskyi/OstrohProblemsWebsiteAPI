using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemCategories.Exceptions;
using Domain.ProblemCategories;
using MediatR;

namespace Application.ProblemCategories.Commands;

public record UpdateProblemCategoryCommand : IRequest<Result<ProblemCategory, ProblemCategoryException>>
{
    public required Guid ProblemCategoryId { get; init; }
    public required string Name { get; init; }
}

public class UpdateProblemCategoryCommandHandler(
    IProblemCategoryRepository problemCategoryRepository)
    : IRequestHandler<UpdateProblemCategoryCommand, Result<ProblemCategory, ProblemCategoryException>>
{
    public async Task<Result<ProblemCategory, ProblemCategoryException>> Handle(
        UpdateProblemCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var problemCategoryId = new ProblemCategoryId(request.ProblemCategoryId);
        var existingProblemCategory = await problemCategoryRepository.GetById(problemCategoryId, cancellationToken);

        return await existingProblemCategory.Match<Task<Result<ProblemCategory, ProblemCategoryException>>>(
            async problemCategory => await UpdateProblemCategory(problemCategory, request.Name, cancellationToken),
            () => Task.FromResult<Result<ProblemCategory, ProblemCategoryException>>(
                new ProblemCategoryNotFoundException(problemCategoryId))
        );
    }

    private async Task<Result<ProblemCategory, ProblemCategoryException>> UpdateProblemCategory(
        ProblemCategory problemCategory,
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            problemCategory.UpdateName(name);
            return await problemCategoryRepository.Update(problemCategory, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemCategoryUnknownException(problemCategory.Id, exception);
        }
    }
}
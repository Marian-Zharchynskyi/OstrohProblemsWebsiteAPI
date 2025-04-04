using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemCategories.Exceptions;
using Domain.ProblemCategories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.ProblemCategories.Commands;

public record DeleteProblemCategoryCommand : IRequest<Result<ProblemCategory, ProblemCategoryException>>
{
    public required Guid ProblemCategoryId { get; init; }
}

public class DeleteProblemCategoryCommandHandler(
    IProblemCategoryRepository problemCategoryRepository)
    : IRequestHandler<DeleteProblemCategoryCommand, Result<ProblemCategory, ProblemCategoryException>>
{
    public async Task<Result<ProblemCategory, ProblemCategoryException>> Handle(
        DeleteProblemCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var problemCategoryId = new ProblemCategoryId(request.ProblemCategoryId);
        var existingProblemCategory = await problemCategoryRepository.GetById(problemCategoryId, cancellationToken);

        return await existingProblemCategory.Match<Task<Result<ProblemCategory, ProblemCategoryException>>>(
            async problemCategory => await DeleteEntity(problemCategory, cancellationToken),
            () => Task.FromResult<Result<ProblemCategory, ProblemCategoryException>>(
                new ProblemCategoryNotFoundException(problemCategoryId)));
    }

    private async Task<Result<ProblemCategory, ProblemCategoryException>> DeleteEntity(
        ProblemCategory problemCategory,
        CancellationToken cancellationToken)
    {
        try
        {
            return await problemCategoryRepository.Delete(problemCategory, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new ProblemCategoryHasRelatedProblemsException(problemCategory.Id);
        }
        catch (Exception exception)
        {
            return new ProblemCategoryUnknownException(problemCategory.Id, exception);
        }
    }
}

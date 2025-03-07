using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemStatuses.Exceptions;
using Domain.ProblemStatuses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.ProblemStatuses.Commands;

public record DeleteProblemStatusCommand : IRequest<Result<ProblemStatus, ProblemStatusException>>
{
    public required Guid ProblemStatusId { get; init; }
}

public class DeleteProblemStatusCommandHandler(
    IProblemStatusRepository problemStatusRepository)
    : IRequestHandler<DeleteProblemStatusCommand, Result<ProblemStatus, ProblemStatusException>>
{
    public async Task<Result<ProblemStatus, ProblemStatusException>> Handle(
        DeleteProblemStatusCommand request,
        CancellationToken cancellationToken)
    {
        var problemStatusId = new ProblemStatusId(request.ProblemStatusId);
        var existingProblemStatus = await problemStatusRepository.GetById(problemStatusId, cancellationToken);

        return await existingProblemStatus.Match<Task<Result<ProblemStatus, ProblemStatusException>>>(
            async problemStatus => await DeleteEntity(problemStatus, cancellationToken),
            () => Task.FromResult<Result<ProblemStatus, ProblemStatusException>>
                (new ProblemStatusNotFoundException(problemStatusId)));
    }

    private async Task<Result<ProblemStatus, ProblemStatusException>> DeleteEntity(
        ProblemStatus problemStatus,
        CancellationToken cancellationToken)
    {
        try
        {
            return await problemStatusRepository.Delete(problemStatus, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new ProblemStatusHasRelatedProductsException(problemStatus.Id);
        }
        catch (Exception exception)
        {
            return new ProblemStatusUnknownException(problemStatus.Id, exception);
        }
    }
}
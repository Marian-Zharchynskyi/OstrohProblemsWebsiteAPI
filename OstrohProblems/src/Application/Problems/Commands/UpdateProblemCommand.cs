using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemStatuses.Exceptions;
using Domain.ProblemStatuses;
using MediatR;

namespace Application.Problems.Commands;

public record UpdateProblemCommand : IRequest<Result<ProblemStatus, ProblemStatusException>>
{
    public required Guid ProblemStatusId { get; init; }
    public required string Name { get; init; }
}

public class UpdateProblemStatusCommandHandler(
    IProblemStatusRepository problemStatusRepository)
    : IRequestHandler<UpdateProblemCommand, Result<ProblemStatus, ProblemStatusException>>
{
    public async Task<Result<ProblemStatus, ProblemStatusException>> Handle(
        UpdateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemStatusId = new ProblemStatusId(request.ProblemStatusId);
        var existingProblemStatus = await problemStatusRepository.GetById(problemStatusId, cancellationToken);

        return await existingProblemStatus.Match<Task<Result<ProblemStatus, ProblemStatusException>>>(
            async problemStatus => await UpdateProblemStatus(problemStatus, request.Name, cancellationToken),
            () => Task.FromResult<Result<ProblemStatus, ProblemStatusException>>(
                new ProblemStatusNotFoundException(problemStatusId))
        );
    }

    private async Task<Result<ProblemStatus, ProblemStatusException>> UpdateProblemStatus(
        ProblemStatus problemStatus,
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            problemStatus.UpdateName(name);
            return await problemStatusRepository.Update(problemStatus, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemStatusUnknownException(problemStatus.Id, exception);
        }
    }
}
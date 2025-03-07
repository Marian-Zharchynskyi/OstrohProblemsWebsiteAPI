using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemStatuses.Exceptions;
using Domain.ProblemStatuses;
using MediatR;

namespace Application.ProblemStatuses.Commands;

public record CreateProblemStatusCommand : IRequest<Result<ProblemStatus, ProblemStatusException>>
{
    public required string Name { get; init; }
}

public class CreateProblemStatusCommandHandler(
    IProblemStatusRepository problemStatusRepository)
    : IRequestHandler<CreateProblemStatusCommand, Result<ProblemStatus, ProblemStatusException>>
{
    public async Task<Result<ProblemStatus, ProblemStatusException>> Handle(
        CreateProblemStatusCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblemStatus = await problemStatusRepository.
            SearchByName(request.Name, cancellationToken);

        return await existingProblemStatus.Match(
            c => Task.FromResult<Result<ProblemStatus, ProblemStatusException>>(
                new ProblemStatusAlreadyExistsException(c.Id)),
            async () => await CreateEntity(request.Name, cancellationToken));
    }

    private async Task<Result<ProblemStatus, ProblemStatusException>> CreateEntity(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProblemStatus.New(ProblemStatusId.New(), name);

            return await problemStatusRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemStatusUnknownException(ProblemStatusId.Empty, exception);
        }
    }
}
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Problems;
using Domain.ProblemStatuses;
using MediatR;

namespace Application.Problems.Commands;

public record CreateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required string Title { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get;init; }
    public required string Description { get; init; }
    public required ProblemStatusId ProblemStatusId { get; init; }
    public List<Guid>? ProblemCategoryIds { get; init; }
}

public class CreateProblemCommandHandler(
    IProblemRepository problemRepository,
    IProblemCategoryRepository problemCategoryRepository)
    : IRequestHandler<CreateProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        CreateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblem = await problemRepository.
            SearchByTitle(request.Title, cancellationToken);

        return await existingProblem.Match(
            c => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemAlreadyExistsException(c.Id)),
            async () => await CreateEntity(
                request.Title,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.ProblemStatusId,
                request.ProblemCategoryIds,
                cancellationToken));
    }

    private async Task<Result<Problem, ProblemException>> CreateEntity(
        string title,
        double latitude,
        double longitude,
        string description,
        ProblemStatusId problemStatusId,
        List<Guid>? problemCategoryIds,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Problem.New(
                ProblemId.New(),
                title,
                latitude,
                longitude,
                description,
                problemStatusId);

            if (problemCategoryIds is not null && problemCategoryIds.Count > 0)
            {
                var categories = await problemCategoryRepository.GetCategoriesByIdsAsync(problemCategoryIds, cancellationToken);

                foreach (var category in categories)
                {
                    entity.AddCategory(category);
                }
            }

            return await problemRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemUnknownException(ProblemId.Empty, exception);
        }
    }
}
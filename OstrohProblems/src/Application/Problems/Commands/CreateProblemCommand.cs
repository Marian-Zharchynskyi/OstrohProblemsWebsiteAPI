using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Statuses;
using MediatR;

namespace Application.Problems.Commands;

public record CreateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required string Title { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string Description { get; init; }
    public required StatusId StatusId { get; init; }
    public required List<Guid> ProblemCategoryIds { get; init; }
}

public class CreateProblemCommandHandler(
    IProblemRepository problemRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<CreateProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        CreateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblem = await problemRepository.SearchByTitle(request.Title, cancellationToken);

        return await existingProblem.Match(
            p => Task.FromResult<Result<Problem, ProblemException>>(new ProblemAlreadyExistsException(p.Id)),
            async () => await CreateEntity(
                request.Title,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.StatusId,
                request.ProblemCategoryIds,
                cancellationToken));
    }

    private async Task<Result<Problem, ProblemException>> CreateEntity(
        string title,
        double latitude,
        double longitude,
        string description,
        StatusId statusId,
        List<Guid> categoryIds,
        CancellationToken cancellationToken)
    {
        try
        {
            //TODO: add user id
            var problem = Problem.New(
                ProblemId.New(),
                title,
                latitude,
                longitude,
                description,
                statusId,
                UserId.Empty
                );

            if (categoryIds.Any())
            {
                var categories = await categoryRepository.GetByIdsAsync(categoryIds, cancellationToken);

                foreach (var category in categories)
                {
                    problem.AddCategory(category);
                }
            }

            return await problemRepository.Add(problem, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ProblemUnknownException(ProblemId.Empty, ex);
        }
    }
}


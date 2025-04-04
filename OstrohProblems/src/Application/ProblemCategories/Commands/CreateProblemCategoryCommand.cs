using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProblemCategories.Exceptions;
using Domain.ProblemCategories;
using MediatR;

namespace Application.ProblemCategories.Commands;

public record CreateProblemCategoryCommand : IRequest<Result<ProblemCategory, ProblemCategoryException>>
{
    public required string Name { get; init; }
}

public class CreateProblemCategoryCommandHandler(
    IProblemCategoryRepository problemCategoryRepository)
    : IRequestHandler<CreateProblemCategoryCommand, Result<ProblemCategory, ProblemCategoryException>>
{
    public async Task<Result<ProblemCategory, ProblemCategoryException>> Handle(
        CreateProblemCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblemCategory = await problemCategoryRepository.SearchByName(request.Name, cancellationToken);

        return await existingProblemCategory.Match(
            c => Task.FromResult<Result<ProblemCategory, ProblemCategoryException>>(
                new ProblemCategoryAlreadyExistsException(c.Id)),
            async () => await CreateEntity(request.Name, cancellationToken));
    }

    private async Task<Result<ProblemCategory, ProblemCategoryException>> CreateEntity(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProblemCategory.New(ProblemCategoryId.New(), name);

            return await problemCategoryRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemCategoryUnknownException(ProblemCategoryId.Empty, exception);
        }
    }
}
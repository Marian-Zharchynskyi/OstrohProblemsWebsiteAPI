using Domain.ProblemCategories;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemCategoryQueries
{
    Task<IReadOnlyList<ProblemCategory>> GetAll(CancellationToken cancellationToken);
    Task<Option<ProblemCategory>> GetById(ProblemCategoryId id, CancellationToken cancellationToken);
}
using Domain.ProblemCategories;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProblemCategoryRepository
{
    Task<Option<ProblemCategory>> GetById(ProblemCategoryId id, CancellationToken cancellationToken);
    Task<Option<ProblemCategory>> SearchByName(string name, CancellationToken cancellationToken);
    Task<ProblemCategory> Add(ProblemCategory problemStatus, CancellationToken cancellationToken);
    Task<ProblemCategory> Update(ProblemCategory problemStatus, CancellationToken cancellationToken);
    Task<ProblemCategory> Delete(ProblemCategory problemStatus, CancellationToken cancellationToken);
}
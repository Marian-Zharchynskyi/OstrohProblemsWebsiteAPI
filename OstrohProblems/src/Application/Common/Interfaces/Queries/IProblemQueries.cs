using Domain.Problems;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemQueries
{
    Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken);
    Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken);
}
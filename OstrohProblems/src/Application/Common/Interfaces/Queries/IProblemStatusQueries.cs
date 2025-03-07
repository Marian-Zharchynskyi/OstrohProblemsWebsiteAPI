using Domain.ProblemStatuses;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemStatusQueries
{
    Task<IReadOnlyList<ProblemStatus>> GetAll(CancellationToken cancellationToken);
    Task<Option<ProblemStatus>> GetById(ProblemStatusId id, CancellationToken cancellationToken);
}
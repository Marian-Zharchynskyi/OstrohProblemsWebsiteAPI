using Domain.Statuses;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemStatusQueries
{
    Task<IReadOnlyList<Status>> GetAll(CancellationToken cancellationToken);
    Task<Option<Status>> GetById(StatusId id, CancellationToken cancellationToken);
}
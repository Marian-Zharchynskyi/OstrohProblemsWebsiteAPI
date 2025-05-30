using Domain.Ratings;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IRatingQueries
{
    Task<IReadOnlyList<Rating>> GetAll(CancellationToken cancellationToken);
    Task<Option<Rating>> GetById(RatingId id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Rating> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken);
}

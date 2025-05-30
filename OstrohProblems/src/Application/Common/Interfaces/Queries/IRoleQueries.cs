using Domain.Identity.Roles;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IRoleQueries
{
    Task<IReadOnlyList<Role>> GetAll(CancellationToken cancellationToken);
    Task<Option<Role>> GetByName(string name, CancellationToken cancellationToken);
    Task<Option<Role>> GetById(RoleId id, CancellationToken cancellationToken);
}
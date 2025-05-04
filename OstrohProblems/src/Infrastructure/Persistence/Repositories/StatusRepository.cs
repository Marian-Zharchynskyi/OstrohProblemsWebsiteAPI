using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Statuses;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository : IProblemStatusQueries , IProblemStatusRepository
{
    private readonly ApplicationDbContext _context;

    public StatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Status>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Statuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Status>> GetById(StatusId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Statuses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Status>() : Option.Some(entity);
    }

    public async Task<Option<Status>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.Statuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Status>() : Option.Some(entity);
    }

    public async Task<Status> Add(Status manufacturer, CancellationToken cancellationToken)
    {
        await _context.Statuses.AddAsync(manufacturer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<Status> Update(Status manufacturer, CancellationToken cancellationToken)
    {
        _context.Statuses.Update(manufacturer);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<Status> Delete(Status manufacturer, CancellationToken cancellationToken)
    {
        _context.Statuses.Remove(manufacturer);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }
}
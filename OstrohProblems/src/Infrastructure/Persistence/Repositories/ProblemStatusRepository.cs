using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProblemStatuses;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemStatusRepository : IProblemStatusQueries , IProblemStatusRepository
{
    private readonly ApplicationDbContext _context;

    public ProblemStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProblemStatus>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.ProblemStatuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<ProblemStatus>> GetById(ProblemStatusId id, CancellationToken cancellationToken)
    {
        var entity = await _context.ProblemStatuses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ProblemStatus>() : Option.Some(entity);
    }

    public async Task<Option<ProblemStatus>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.ProblemStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<ProblemStatus>() : Option.Some(entity);
    }

    public async Task<ProblemStatus> Add(ProblemStatus manufacturer, CancellationToken cancellationToken)
    {
        await _context.ProblemStatuses.AddAsync(manufacturer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<ProblemStatus> Update(ProblemStatus manufacturer, CancellationToken cancellationToken)
    {
        _context.ProblemStatuses.Update(manufacturer);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<ProblemStatus> Delete(ProblemStatus manufacturer, CancellationToken cancellationToken)
    {
        _context.ProblemStatuses.Remove(manufacturer);
        await _context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }
}
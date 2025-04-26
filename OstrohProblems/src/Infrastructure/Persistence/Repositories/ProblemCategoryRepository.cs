using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProblemCategories;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemCategoryRepository : IProblemCategoryRepository, IProblemCategoryQueries
{
    private readonly ApplicationDbContext _context;

    public ProblemCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProblemCategory>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.ProblemCategories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<ProblemCategory>> GetById(ProblemCategoryId id, CancellationToken cancellationToken)
    {
        var entity = await _context.ProblemCategories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ProblemCategory>() : Option.Some(entity);
    }

    public async Task<Option<ProblemCategory>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.ProblemCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<ProblemCategory>() : Option.Some(entity);
    }

    public async Task<ProblemCategory> Add(ProblemCategory category, CancellationToken cancellationToken)
    {
        await _context.ProblemCategories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<ProblemCategory> Update(ProblemCategory category, CancellationToken cancellationToken)
    {
        _context.ProblemCategories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<ProblemCategory> Delete(ProblemCategory category, CancellationToken cancellationToken)
    {
        _context.ProblemCategories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }
    
    public async Task<List<ProblemCategory>> GetCategoriesByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.ProblemCategories
            .Where(c => ids.Contains(c.Id.Value))
            .ToListAsync(cancellationToken);
    }
}

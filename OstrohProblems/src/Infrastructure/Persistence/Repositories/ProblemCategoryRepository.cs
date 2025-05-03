using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProblemCategories;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemCategoryRepository(ApplicationDbContext context)
    : IProblemCategoryRepository, IProblemCategoryQueries
{
    public async Task<IReadOnlyList<ProblemCategory>> GetAll(CancellationToken cancellationToken)
    {
        return await context.ProblemCategories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<ProblemCategory>> GetById(ProblemCategoryId id, CancellationToken cancellationToken)
    {
        var entity = await context.ProblemCategories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ProblemCategory>() : Option.Some(entity);
    }

    public async Task<Option<ProblemCategory>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.ProblemCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<ProblemCategory>() : Option.Some(entity);
    }

    public async Task<ProblemCategory> Add(ProblemCategory category, CancellationToken cancellationToken)
    {
        await context.ProblemCategories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<ProblemCategory> Update(ProblemCategory category, CancellationToken cancellationToken)
    {
        context.ProblemCategories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<ProblemCategory> Delete(ProblemCategory category, CancellationToken cancellationToken)
    {
        context.ProblemCategories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }
    
    public async Task<List<ProblemCategory>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var problemCategoryIds = ids.Select(id => new ProblemCategoryId(id)).ToList();

        return await context.ProblemCategories
            .Where(pc => problemCategoryIds.Contains(pc.Id)) 
            .ToListAsync(cancellationToken);
    }


}

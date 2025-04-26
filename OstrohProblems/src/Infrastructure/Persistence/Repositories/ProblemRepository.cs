using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProblemCategories;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemRepository(ApplicationDbContext context) : IProblemQueries, IProblemRepository
{
    public async Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Categories)
            .Include(x => x.Comments)
            .Include(x => x.ProblemStatus)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken)
    {
        var entity = await context.Problems
            .Include(x => x.Categories)
            .Include(x => x.Comments)
            .Include(x => x.ProblemStatus)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Option<Problem>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Problems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Problem> Add(Problem problem, CancellationToken cancellationToken)
    {
        await context.Problems.AddAsync(problem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<Problem> Update(Problem problem, CancellationToken cancellationToken)
    {
        context.Problems.Update(problem);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }
    
    public async Task<Problem> Delete(Problem problem, CancellationToken cancellationToken)
    {
        context.Problems.Remove(problem);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }

}

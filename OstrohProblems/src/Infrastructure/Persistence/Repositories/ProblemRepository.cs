using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemRepository : IProblemQueries, IProblemRepository
{
    private readonly ApplicationDbContext _context;

    public ProblemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Problems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Problems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Option<Problem>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await _context.Problems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Problem> Add(Problem problem, CancellationToken cancellationToken)
    {
        await _context.Problems.AddAsync(problem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<Problem> Update(Problem problem, CancellationToken cancellationToken)
    {
        _context.Problems.Update(problem);
        await _context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<Problem> Delete(Problem problem, CancellationToken cancellationToken)
    {
        _context.Problems.Remove(problem);
        await _context.SaveChangesAsync(cancellationToken);
        return problem;
    }
}

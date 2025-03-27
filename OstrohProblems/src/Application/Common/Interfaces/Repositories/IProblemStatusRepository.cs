using Domain.ProblemStatuses;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProblemStatusRepository
{
    Task<Option<ProblemStatus>> GetById(ProblemStatusId id, CancellationToken cancellationToken);
    Task<Option<ProblemStatus>> SearchByName(string name, CancellationToken cancellationToken);
    Task<ProblemStatus> Add(ProblemStatus problemStatus, CancellationToken cancellationToken);
    Task<ProblemStatus> Update(ProblemStatus problemStatus, CancellationToken cancellationToken);
    Task<ProblemStatus> Delete(ProblemStatus problemStatus, CancellationToken cancellationToken);
}
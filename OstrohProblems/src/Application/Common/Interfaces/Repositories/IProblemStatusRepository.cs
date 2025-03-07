using Domain.ProblemStatuses;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProblemStatusRepository
{
    Task<Option<ProblemStatus>> GetById(ProblemStatusId id, CancellationToken cancellationToken);
    Task<Option<ProblemStatus>> SearchByName(string name, CancellationToken cancellationToken);
    Task<ProblemStatus> Add(ProblemStatus manufacturer, CancellationToken cancellationToken);
    Task<ProblemStatus> Update(ProblemStatus manufacturer, CancellationToken cancellationToken);
    Task<ProblemStatus> Delete(ProblemStatus manufacturer, CancellationToken cancellationToken);
}
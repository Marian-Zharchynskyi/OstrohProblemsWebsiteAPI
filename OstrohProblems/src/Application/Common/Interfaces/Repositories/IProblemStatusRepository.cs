﻿using Domain.Statuses;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProblemStatusRepository
{
    Task<Option<Status>> GetById(StatusId id, CancellationToken cancellationToken);
    Task<Option<Status>> SearchByName(string name, CancellationToken cancellationToken);
    Task<Status> Add(Status status, CancellationToken cancellationToken);
    Task<Status> Update(Status status, CancellationToken cancellationToken);
    Task<Status> Delete(Status status, CancellationToken cancellationToken);
}
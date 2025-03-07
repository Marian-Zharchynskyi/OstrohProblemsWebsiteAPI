using Domain.ProblemStatuses;

namespace Application.ProblemStatuses.Exceptions;


public abstract class ProblemStatusException(ProblemStatusId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProblemStatusId ManufacturerId { get; } = id;
}

public class ProblemStatusNotFoundException(ProblemStatusId id) 
    : ProblemStatusException(id, $"ProblemStatus under id: {id} not found");

public class ProblemStatusAlreadyExistsException(ProblemStatusId id) 
    : ProblemStatusException(id, $"ProblemStatus already exists: {id}");

public class ProblemStatusUnknownException(ProblemStatusId id, Exception innerException)
    : ProblemStatusException(id, $"Unknown exception for the ProblemStatus under id: {id}", innerException);
    
public class ProblemStatusHasRelatedProductsException(ProblemStatusId id)
    : ProblemStatusException(id, $"ProblemStatus with ID {id} cannot be deleted because it has related products.");
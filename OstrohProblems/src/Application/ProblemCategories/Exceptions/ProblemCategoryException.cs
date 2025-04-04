using Domain.ProblemCategories;

namespace Application.ProblemCategories.Exceptions;

public abstract class ProblemCategoryException(ProblemCategoryId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProblemCategoryId CategoryId { get; } = id;
}

public class ProblemCategoryNotFoundException(ProblemCategoryId id)
    : ProblemCategoryException(id, $"ProblemCategory under id: {id} not found");

public class ProblemCategoryAlreadyExistsException(ProblemCategoryId id)
    : ProblemCategoryException(id, $"ProblemCategory already exists: {id}");

public class ProblemCategoryUnknownException(ProblemCategoryId id, Exception innerException)
    : ProblemCategoryException(id, $"Unknown exception for the ProblemCategory under id: {id}", innerException);

public class ProblemCategoryHasRelatedProblemsException(ProblemCategoryId id)
    : ProblemCategoryException(id, $"ProblemCategory with ID {id} cannot be deleted because it has related problems.");
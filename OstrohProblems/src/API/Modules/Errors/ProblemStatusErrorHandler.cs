using Application.ProblemStatuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProblemStatusErrorHandler
{
    public static ObjectResult ToObjectResult(this ProblemStatusException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProblemStatusNotFoundException => StatusCodes.Status404NotFound,
                ProblemStatusAlreadyExistsException => StatusCodes.Status409Conflict,
                ProblemStatusUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Problem status error handler does not implemented")
            }
        };
    }
}
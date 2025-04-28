using Application.Problems.Exceptions;
using Application.ProblemStatuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProblemErrorHandler
{
    public static ObjectResult ToObjectResult(this ProblemException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProblemNotFoundException
                    or ImageNotFoundException 
                    => StatusCodes.Status404NotFound,
                
                ProblemAlreadyExistsException 
                    => StatusCodes.Status409Conflict,
                
                ProblemUnknownException 
                    or ImageSaveException 
                    => StatusCodes.Status500InternalServerError,
                
                _ => throw new NotImplementedException("Problem error handler does not implemented")
            }
        };
    }
}
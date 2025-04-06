using Application.ProblemCategories.Exceptions;
using Application.Problems.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProblemCategoryErrorHandler
{
    public static ObjectResult ToObjectResult(this ProblemCategoryException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProblemCategoryNotFoundException => StatusCodes.Status404NotFound,
                ProblemCategoryAlreadyExistsException => StatusCodes.Status409Conflict,
                ProblemCategoryUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Problem category error handler does not implemented")
            }
        };
    }
}
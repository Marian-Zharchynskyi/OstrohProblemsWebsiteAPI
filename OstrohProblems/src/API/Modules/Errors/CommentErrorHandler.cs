using Application.Comments.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class CommentErrorHandler
{
    public static ObjectResult ToObjectResult(this CommentException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                CommentNotFoundException => StatusCodes.Status404NotFound,
                CommentAlreadyExistsException => StatusCodes.Status409Conflict,
                CommentUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Comment error handler does not implemented")
            }
        };
    }
}
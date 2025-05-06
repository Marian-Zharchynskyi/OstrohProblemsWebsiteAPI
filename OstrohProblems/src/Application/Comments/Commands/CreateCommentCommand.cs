using Application.Comments.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Comments;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;

namespace Application.Comments.Commands;

public class CreateCommentCommand : IRequest<Result<Comment, CommentException>>
{
    public required string Content { get; init; }
    public required Guid ProblemId { get; init; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<Comment, CommentException>>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Result<Comment, CommentException>> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var commentId = CommentId.New();
        var problemId = new ProblemId(request.ProblemId);

        try
        {
            //TODO: add user id
            var comment = Comment.New(commentId, request.Content, problemId, UserId.Empty);
            return await _commentRepository.Add(comment, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CommentUnknownException(commentId, exception);
        }
    }
}
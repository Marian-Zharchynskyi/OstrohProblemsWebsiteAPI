using Domain.Comments;

namespace API.DTOs;

public record CommentDto(Guid? Id, string Content, Guid ProblemId)
{
    public static CommentDto FromDomainModel(Comment comment)
        => new(
            comment.Id.Value,
            comment.Content,
            comment.ProblemId.Value
        );
}
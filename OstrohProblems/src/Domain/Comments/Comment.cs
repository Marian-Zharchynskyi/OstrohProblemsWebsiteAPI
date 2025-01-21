using Domain.Problems;

namespace Domain.Comments;

public class Comment
{
    public CommentId Id { get; }
    public ProblemId ProblemId { get; private set; }  
    public Problem? Problem { get; private set; }  
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Comment(CommentId id, ProblemId problemId, string content, DateTime createdAt)
    {
        Id = id;
        ProblemId = problemId;
        Content = content;
        CreatedAt = createdAt;
    }

    public static Comment New(CommentId id, ProblemId problemId, string content)
    {
        return new Comment(id, problemId, content, DateTime.UtcNow);
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }
}
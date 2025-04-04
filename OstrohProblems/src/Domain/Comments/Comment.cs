using Domain.Problems;

namespace Domain.Comments;

public class Comment
{
    public CommentId Id { get; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ProblemId ProblemId { get; private set; }  
    public Problem? Problem { get; set; }  

    private Comment(CommentId id, string content, DateTime createdAt, ProblemId problemId)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        ProblemId = problemId;
    }

    public static Comment New(CommentId id, string content, ProblemId problemId)
    {
        return new Comment(id, content, DateTime.UtcNow, problemId);
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }
}
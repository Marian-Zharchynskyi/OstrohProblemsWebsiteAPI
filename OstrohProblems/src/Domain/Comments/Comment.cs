namespace Domain.Comments;

public class Comment
{
    public CommentId Id { get; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Comment(CommentId id, string content, DateTime createdAt)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
    }

    public static Comment New(CommentId id, string content)
    {
        return new Comment(id, content, DateTime.UtcNow);
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }
}
using Domain.Problems;

namespace Domain.ProblemRatings;

public class ProblemRating
{
    public ProblemRatingId Id { get; }
    public ProblemId ProblemId { get; private set; }
    public Problem? Problem { get; set; }
    public Guid UserId { get; private set; }
    public double Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ProblemRating(ProblemRatingId id, ProblemId problemId, Guid userId, double rating,
        DateTime createdAt)
    {
        Id = id;
        ProblemId = problemId;
        UserId = userId;
        SetRating(rating);
        CreatedAt = createdAt;
    }
    
    public static ProblemRating New(ProblemRatingId id, ProblemId problemId, Guid userId, double rating)
    {
        return new ProblemRating(id, problemId, userId, rating, DateTime.UtcNow);
    }

    private void SetRating(double value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 5");

        Rating = value;
    }
}
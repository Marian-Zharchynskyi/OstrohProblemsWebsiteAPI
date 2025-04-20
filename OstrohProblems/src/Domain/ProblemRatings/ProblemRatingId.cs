namespace Domain.ProblemRatings;

public record ProblemRatingId(Guid Value)
{
    public static ProblemRatingId New() => new(Guid.NewGuid());
    public static ProblemRatingId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
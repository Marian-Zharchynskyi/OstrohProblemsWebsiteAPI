namespace Domain.ProblemStatuses;

public record ProblemStatusId(Guid Value)
{
    public static ProblemStatusId New() => new(Guid.NewGuid());
    public static ProblemStatusId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
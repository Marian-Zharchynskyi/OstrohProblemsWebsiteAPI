namespace Domain.ProblemCategories;

public record ProblemCategoryId(Guid Value)
{
    public static ProblemCategoryId New() => new(Guid.NewGuid());
    public static ProblemCategoryId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
using Domain.Problems;

namespace Domain.ProblemCategories;

public class ProblemCategory
{
    public ProblemCategoryId Id { get; }
    public string Name { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();

    private ProblemCategory(ProblemCategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ProblemCategory New(ProblemCategoryId id, string name)
    {
        return new ProblemCategory(id, name);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
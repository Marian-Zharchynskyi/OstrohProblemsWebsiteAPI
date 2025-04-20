using Domain.Problems;

namespace Domain.ProblemStatuses;

public class ProblemStatus
{
    public ProblemStatusId Id { get; }
    public string Name { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();

    private ProblemStatus(ProblemStatusId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ProblemStatus New(ProblemStatusId id, string name)
    {
        return new ProblemStatus(id, name);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
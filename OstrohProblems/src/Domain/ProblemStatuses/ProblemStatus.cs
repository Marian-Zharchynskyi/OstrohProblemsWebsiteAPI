namespace Domain.ProblemStatuses;

public class ProblemStatus
{
    public ProblemStatusId Id { get; }
    public string Name { get; private set; }

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
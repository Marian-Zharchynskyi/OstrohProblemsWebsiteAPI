using Domain.Comments;
using Domain.ProblemCategories;
using Domain.ProblemStatuses;

namespace Domain.Problems;

public class Problem
{
    public ProblemId Id { get; }
    public string Title { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Description { get; private set; }
    public ProblemStatusId ProblemStatusId { get; private set; }
    public ProblemStatus? ProblemStatus { get; set; }
    public List<Comment> Comments { get; private set; } = new();
    public List<ProblemCategory> Categories { get; private set; } = new();

    private Problem(ProblemId id, string title, double latitude, double longitude, string description,
        ProblemStatusId problemStatusId)
    {
        Id = id;
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        ProblemStatusId = problemStatusId;
    }

    public static Problem New(ProblemId id, string title, double latitude, double longitude, string description,
        ProblemStatusId problemStatusId)
    {
        return new Problem(id, title, latitude, longitude, description, problemStatusId);
    }
}
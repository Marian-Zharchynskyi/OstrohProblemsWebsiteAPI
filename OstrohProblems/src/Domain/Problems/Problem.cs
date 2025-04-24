using Domain.Comments;
using Domain.ProblemCategories;
using Domain.ProblemRatings;
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
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<ProblemCategory> Categories { get; private set; } = new List<ProblemCategory>();
    public ICollection<ProblemRating> Ratings { get; private set; } = new List<ProblemRating>();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Problem(ProblemId id, string title, double latitude, double longitude, string description,
        ProblemStatusId problemStatusId, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        ProblemStatusId = problemStatusId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Problem New(ProblemId id, string title, double latitude, double longitude, string description,
        ProblemStatusId problemStatusId)
    {
        return new Problem(id, title, latitude, longitude, description, problemStatusId, DateTime.UtcNow, DateTime.UtcNow);
    }

    public void UpdateProblem(string title, double latitude, double longitude, string description, ProblemStatusId problemStatusId)
    {
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        ProblemStatusId = problemStatusId;
        UpdatedAt = DateTime.UtcNow;
    }
}
using Domain.Comments;
using Domain.Locations;
using Domain.ProblemStatuses;
using Domain.ProblemCategories;

namespace Domain.Problems;

public class Problem
{
    public ProblemId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public LocationId LocationId { get; private set; }
    public Location? Location { get; private set; }
    public ProblemStatusId ProblemStatusId { get; private set; }
    public ProblemStatus? ProblemStatus { get; private set; }
    public List<Comment> Comments { get; private set; } = new();
    public List<ProblemCategory> Categories { get; private set; } = new();  

    private Problem(ProblemId id, string title, string description, LocationId locationId,
        Location? location, ProblemStatusId problemStatusId, ProblemStatus? problemStatus)
    {
        Id = id;
        Title = title;
        Description = description;
        LocationId = locationId;
        Location = location;
        ProblemStatusId = problemStatusId;
        ProblemStatus = problemStatus;
    }

    public static Problem New(ProblemId id, string title, string description, LocationId locationId,
        Location? location, ProblemStatusId problemStatusId, ProblemStatus? problemStatus)
    {
        return new Problem(id, title, description, locationId, location, problemStatusId, problemStatus);
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public void AddCategory(ProblemCategory category)
    {
        Categories.Add(category);
    }
}
namespace Domain.Problems;

public class Problem
{
    public ProblemId Id { get; } 
    public string Title { get; private set; } 
    public double Latitude { get; private set; } 
    public double Longitude { get; private set; } 

    private Problem(ProblemId id, string title, double latitude, double longitude)
    {
        Id = id;
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Problem New(ProblemId id, string title, double latitude, double longitude)
    {
        return new Problem(id, title, latitude, longitude);
    }
}
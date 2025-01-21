namespace Domain.Locations;

public class Location
{
    public LocationId Id { get; }
    public string Name { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    private Location(LocationId id, string name, double latitude, double longitude)
    {
        Id = id;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location New(LocationId id, string name, double latitude, double longitude)
    {
        return new Location(id, name, latitude, longitude);
    }

    public void UpdateLocation(string name, double latitude, double longitude)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }
}
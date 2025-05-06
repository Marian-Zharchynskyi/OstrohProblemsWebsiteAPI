using Domain.Identity.Users;

namespace Domain.Identity.Roles;

public class Role
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    
    private Role(string name)
    {
        Id = name;
        Name = name;
    }
    public static Role New(string name)
        => new(name);
}
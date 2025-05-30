using Domain.Identity.Users;

namespace API.DTOs.Identity;

public record SignUpDto(string Email, string Password, string? Name)
{
    public static SignUpDto FromDomainModel(User user)
        => new(user.Email, user.PasswordHash, user.FullName);
}
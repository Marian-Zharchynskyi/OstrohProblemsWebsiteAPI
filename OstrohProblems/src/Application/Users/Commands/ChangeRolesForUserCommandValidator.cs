using FluentValidation;

namespace Application.Users.Commands;

public class ChangeRolesForUserCommandValidator : AbstractValidator<ChangeRolesForUserCommand>
{
    public ChangeRolesForUserCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("User ID cannot be null or empty.");

        RuleFor(command => command.RoleIds)
            .NotNull()
            .NotEmpty()
            .WithMessage(command => $"User under ID: {command.UserId} must have at least one role!");
    }
}
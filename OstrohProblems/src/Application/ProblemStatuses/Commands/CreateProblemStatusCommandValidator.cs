using FluentValidation;

namespace Application.ProblemStatuses.Commands;

public class CreateProblemStatusCommandValidator: AbstractValidator<CreateProblemStatusCommand>
{
    public CreateProblemStatusCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Name must be between 3 and 255 characters.");
    }
}
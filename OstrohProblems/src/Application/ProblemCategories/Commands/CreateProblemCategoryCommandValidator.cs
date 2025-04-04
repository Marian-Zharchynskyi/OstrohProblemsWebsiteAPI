using FluentValidation;

namespace Application.ProblemCategories.Commands;

public class CreateProblemCategoryCommandValidator: AbstractValidator<CreateProblemCategoryCommand>
{
    public CreateProblemCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Name must be between 3 and 255 characters.");
    }
}
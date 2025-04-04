using FluentValidation;

namespace Application.ProblemCategories.Commands;

public class UpdateProblemCategoryCommandValidator : AbstractValidator<UpdateProblemCategoryCommand>
{
    public UpdateProblemCategoryCommandValidator()
    {
        RuleFor(x => x.ProblemCategoryId)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);
    }
}
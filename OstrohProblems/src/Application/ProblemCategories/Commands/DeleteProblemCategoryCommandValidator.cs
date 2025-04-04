using FluentValidation;

namespace Application.ProblemCategories.Commands;

public class DeleteProblemCategoryCommandValidator: AbstractValidator<DeleteProblemCategoryCommand>
{
    public DeleteProblemCategoryCommandValidator()
    {
        RuleFor(x => x.ProblemCategoryId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}
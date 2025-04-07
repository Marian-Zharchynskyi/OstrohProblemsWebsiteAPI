using FluentValidation;

namespace Application.Problems.Commands;

public class DeleteProblemCommandValidator: AbstractValidator<DeleteProblemCommand>
{
    public DeleteProblemCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}
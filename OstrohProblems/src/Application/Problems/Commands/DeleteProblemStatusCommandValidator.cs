using FluentValidation;

namespace Application.Problems.Commands;

public class DeleteProblemStatusCommandValidator: AbstractValidator<DeleteProblemStatusCommand>
{
    public DeleteProblemStatusCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}
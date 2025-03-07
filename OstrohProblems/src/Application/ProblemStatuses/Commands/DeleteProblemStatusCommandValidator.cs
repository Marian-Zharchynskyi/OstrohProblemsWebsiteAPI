using FluentValidation;

namespace Application.ProblemStatuses.Commands;

public class DeleteProblemStatusCommandValidator: AbstractValidator<DeleteProblemStatusCommand>
{
    public DeleteProblemStatusCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}
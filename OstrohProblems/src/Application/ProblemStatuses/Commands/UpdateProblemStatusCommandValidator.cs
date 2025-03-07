using FluentValidation;

namespace Application.ProblemStatuses.Commands;

public class UpdateProblemStatusCommandValidator : AbstractValidator<UpdateProblemStatusCommand>
{
    public UpdateProblemStatusCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}
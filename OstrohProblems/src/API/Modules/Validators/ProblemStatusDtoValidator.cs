using API.DTOs;
using FluentValidation;

namespace Api.Modules.Validators;

public class ProblemStatusDtoValidator : AbstractValidator<ProblemStatusDto>
{
    public ProblemStatusDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}
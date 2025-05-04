using API.DTOs;
using FluentValidation;

namespace API.Modules.Validators;

public class ProblemStatusDtoValidator : AbstractValidator<StatusDto>
{
    public ProblemStatusDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}
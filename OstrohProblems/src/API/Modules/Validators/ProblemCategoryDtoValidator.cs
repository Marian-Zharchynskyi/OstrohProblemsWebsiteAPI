using API.DTOs;
using FluentValidation;

namespace Api.Modules.Validators;

public class ProblemCategoryDtoValidator : AbstractValidator<ProblemStatusDto>
{
    public ProblemCategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}
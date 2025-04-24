using Domain.ProblemCategories;

namespace API.DTOs;

public record ProblemCategoryDto(Guid? Id, string Name)
{
    public static ProblemCategoryDto FromDomainModel(ProblemCategory problemCategory)
        => new(
            problemCategory.Id.Value,
            problemCategory.Name
        );
}
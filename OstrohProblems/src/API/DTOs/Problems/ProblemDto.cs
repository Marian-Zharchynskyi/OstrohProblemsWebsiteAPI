using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    ProblemStatusDto? ProblemStatus,
    List<CommentDto>? Comment,
    List<ProblemCategoryDto>? ProblemCategories,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.ProblemStatus == null ? null : ProblemStatusDto.FromDomainModel(problem.ProblemStatus),
            problem.Comments.Count == 0 ? null : problem.Comments.Select(CommentDto.FromDomainModel).ToList(),
            problem.Categories.Count == 0
                ? null
                : problem.Categories.Select(ProblemCategoryDto.FromDomainModel).ToList(),
            problem.CreatedAt,
            problem.UpdatedAt
        );
}
using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    ProblemStatusDto? ProblemStatus,
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
            problem.CreatedAt,
            problem.UpdatedAt
        );
}
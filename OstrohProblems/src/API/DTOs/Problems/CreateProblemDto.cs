using Domain.Problems;

namespace API.DTOs.Problems;

public record CreateProblemDto(
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    Guid ProblemStatusId
    )
{
    public static CreateProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.ProblemStatusId.Value
        );
}
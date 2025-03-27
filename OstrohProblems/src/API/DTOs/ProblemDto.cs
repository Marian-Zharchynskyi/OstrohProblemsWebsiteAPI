using Domain.Problems;

namespace Api.Dtos;

public record ProblemDto(Guid? Id, string Title, double Latitude, double Longitude)
{
    public static ProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude
        );
}
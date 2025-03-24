using Domain.ProblemStatuses;

namespace Api.Dtos;

public record ProblemStatusDto(Guid? Id, string Name)
{
    public static ProblemStatusDto FromDomainModel(ProblemStatus problemStatus)
        => new(
            problemStatus.Id.Value,
            problemStatus.Name
        );
}
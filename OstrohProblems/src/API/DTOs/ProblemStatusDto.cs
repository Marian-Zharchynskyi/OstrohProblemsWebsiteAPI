using Domain.ProblemStatuses;

namespace API.DTOs;

public record ProblemStatusDto(Guid? Id, string Name)
{
    public static ProblemStatusDto FromDomainModel(ProblemStatus problemStatus)
        => new(
            problemStatus.Id.Value,
            problemStatus.Name
        );
}
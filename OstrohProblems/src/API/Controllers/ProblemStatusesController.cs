using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.ProblemStatuses.Commands;
using Domain.ProblemStatuses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("problem-statuses")]
[ApiController]
public class ProblemStatusesController(
    ISender sender,
    IProblemStatusQueries problemStatusQueries) 
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemStatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemStatusQueries.GetAll(cancellationToken);
        return entities.Select(ProblemStatusDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{problemStatusId:guid}")]
    public async Task<ActionResult<ProblemStatusDto>> Get([FromRoute] Guid problemStatusId,
        CancellationToken cancellationToken)
    {
        var entity = await problemStatusQueries.GetById(new ProblemStatusId(problemStatusId), cancellationToken);

        return entity.Match<ActionResult<ProblemStatusDto>>(
            ps => ProblemStatusDto.FromDomainModel(ps),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProblemStatusDto>> Create(
        [FromBody] ProblemStatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateProblemStatusCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemStatusDto>>(
            ps => ProblemStatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProblemStatusDto>> Update(
        [FromBody] ProblemStatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemStatusCommand
        {
            ProblemStatusId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemStatusDto>>(
            ps => ProblemStatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemStatusId:guid}")]
    public async Task<ActionResult<ProblemStatusDto>> Delete(
        [FromRoute] Guid problemStatusId, CancellationToken cancellationToken)
    {
        var input = new DeleteProblemStatusCommand
        {
            ProblemStatusId = problemStatusId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemStatusDto>>(
            ps => ProblemStatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }
}

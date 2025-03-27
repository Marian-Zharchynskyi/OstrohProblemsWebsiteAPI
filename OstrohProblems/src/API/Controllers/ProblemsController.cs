using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Problems.Commands;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("problems")]
[ApiController]
public class ProblemsController(
    ISender sender,
    IProblemQueries problemQueries) 
    : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetAll(cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }

    [AllowAnonymous]
    [HttpGet("get-by-id/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Get([FromRoute] Guid problemId,
        CancellationToken cancellationToken)
    {
        var entity = await problemQueries.GetById(new ProblemId(problemId), cancellationToken);

        return entity.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProblemDto>> Create(
        [FromBody] ProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateProblemCommand
        {
            Title = request.Title,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    // [HttpPut("update")]
    // public async Task<ActionResult<ProblemDto>> Update(
    //     [FromBody] ProblemDto request,
    //     CancellationToken cancellationToken)
    // {
    //     var input = new UpdateProblemCommand
    //     {
    //         ProblemId = request.Id!.Value,
    //         Title = request.Title,
    //         Latitude = request.Latitude,
    //         Longitude = request.Longitude
    //     };
    //
    //     var result = await sender.Send(input, cancellationToken);
    //
    //     return result.Match<ActionResult<ProblemDto>>(
    //         problem => ProblemDto.FromDomainModel(problem),
    //         e => e.ToObjectResult());
    // }
    //
    // [HttpDelete("delete/{problemId:guid}")]
    // public async Task<ActionResult<ProblemDto>> Delete(
    //     [FromRoute] Guid problemId, CancellationToken cancellationToken)
    // {
    //     var input = new DeleteProblemCommand
    //     {
    //         ProblemId = problemId
    //     };
    //
    //     var result = await sender.Send(input, cancellationToken);
    //
    //     return result.Match<ActionResult<ProblemDto>>(
    //         problem => ProblemDto.FromDomainModel(problem),
    //         e => e.ToObjectResult());
    // }
}

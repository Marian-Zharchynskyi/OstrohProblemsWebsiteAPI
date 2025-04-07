using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.ProblemCategories.Commands;
using Domain.ProblemCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("problem-categories")]
[ApiController]
public class ProblemCategoriesController(
    ISender sender,
    IProblemCategoryQueries problemCategoryQueries)
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemCategoryQueries.GetAll(cancellationToken);
        return entities.Select(ProblemCategoryDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{problemCategoryId:guid}")]
    public async Task<ActionResult<ProblemCategoryDto>> Get([FromRoute] Guid problemCategoryId,
        CancellationToken cancellationToken)
    {
        var entity = await problemCategoryQueries.GetById(new ProblemCategoryId(problemCategoryId), cancellationToken);

        return entity.Match<ActionResult<ProblemCategoryDto>>(
            pc => ProblemCategoryDto.FromDomainModel(pc),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProblemCategoryDto>> Create(
        [FromBody] ProblemCategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateProblemCategoryCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemCategoryDto>>(
            pc => ProblemCategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProblemCategoryDto>> Update(
        [FromBody] ProblemCategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemCategoryCommand
        {
            ProblemCategoryId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemCategoryDto>>(
            pc => ProblemCategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemCategoryId:guid}")]
    public async Task<ActionResult<ProblemCategoryDto>> Delete(
        [FromRoute] Guid problemCategoryId, CancellationToken cancellationToken)
    {
        var input = new DeleteProblemCategoryCommand
        {
            ProblemCategoryId = problemCategoryId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemCategoryDto>>(
            pc => ProblemCategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }
}
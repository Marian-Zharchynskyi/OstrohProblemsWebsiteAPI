using API.DTOs;
using API.Modules.Errors;
using Application.Categories.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problem-categories")]
[ApiController]
public class CategoriesController(
    ISender sender,
    IProblemCategoryQueries problemCategoryQueries)
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemCategoryQueries.GetAll(cancellationToken);
        return entities.Select(CategoryDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{problemCategoryId:guid}")]
    public async Task<ActionResult<CategoryDto>> Get([FromRoute] Guid problemCategoryId,
        CancellationToken cancellationToken)
    {
        var entity = await problemCategoryQueries.GetById(new CategoryId(problemCategoryId), cancellationToken);

        return entity.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<CategoryDto>> Create(
        [FromBody] CategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateCategoryCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpPut("update")]
    public async Task<ActionResult<CategoryDto>> Update(
        [FromBody] CategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateCategoryCommand
        {
            ProblemCategoryId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemCategoryId:guid}")]
    public async Task<ActionResult<CategoryDto>> Delete(
        [FromRoute] Guid problemCategoryId, CancellationToken cancellationToken)
    {
        var input = new DeleteCategoryCommand
        {
            ProblemCategoryId = problemCategoryId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }
}
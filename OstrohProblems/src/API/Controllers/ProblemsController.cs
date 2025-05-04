﻿using API.DTOs.Problems;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Problems.Commands;
using Domain.Problems;
using Domain.Statuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problems")]
[ApiController]
public class ProblemsController(
    ISender sender,
    IProblemQueries problemQueries) 
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetAll(cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }

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
    public async Task<ActionResult<CreateProblemDto>> Create(
        [FromBody] CreateProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateProblemCommand
        {
            Title = request.Title,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            StatusId = new StatusId(request.ProblemStatusId),
            ProblemCategoryIds = request.ProblemCategoryIds
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateProblemDto>>(
            problem => CreateProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [HttpPut("update/{problemId:guid}")]
    public async Task<ActionResult<CreateProblemDto>> Update(
        [FromRoute] Guid problemId,
        [FromBody] CreateProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemCommand
        {
            Id = problemId,
            Title = request.Title,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            StatusId = new StatusId(request.ProblemStatusId),
            ProblemCategoryIds = request.ProblemCategoryIds 
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateProblemDto>>(
            problem => CreateProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Delete(
        [FromRoute] Guid problemId, 
        CancellationToken cancellationToken)
    {
        var input = new DeleteProblemCommand
        {
            ProblemId = problemId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }
    
    [HttpPut("upload-images/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Upload([FromRoute] Guid problemId, IFormFileCollection imagesFiles,
        CancellationToken cancellationToken)
    {
        var input = new UploadProblemImagesCommand()
        {
            ProblemId = problemId,
            ImagesFiles = imagesFiles
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [HttpPut("delete-image/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Upload([FromRoute] Guid problemId, Guid problemImageId,
        CancellationToken cancellationToken)
    {
        var input = new DeleteProblemImageCommand()
        {
            ProblemId = problemId,
            ProblemImageId = problemImageId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }
}
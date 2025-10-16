﻿using Application.Categories.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Categories;
using MediatR;

namespace Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<Result<Category, CategoryException>>
{
    public required string Name { get; init; }
}

public class CreateProblemCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand, Result<Category, CategoryException>>
{
    public async Task<Result<Category, CategoryException>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblemCategory = await categoryRepository.SearchByName(request.Name, cancellationToken);

        return await existingProblemCategory.Match(
            c => Task.FromResult<Result<Category, CategoryException>>(
                new CategoryAlreadyExistsException(c.Id)),
            async () => await CreateEntity(request.Name, cancellationToken));
    }

    private async Task<Result<Category, CategoryException>> CreateEntity(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Category.New(CategoryId.New(), name);

            return await categoryRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CategoryUnknownException(CategoryId.Empty, exception);
        }
    }
}
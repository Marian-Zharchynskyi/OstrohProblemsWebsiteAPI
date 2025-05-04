﻿using Domain.Categories;

namespace API.DTOs;

public record CategoryDto(Guid? Id, string Name)
{
    public static CategoryDto FromDomainModel(Category category)
        => new(
            category.Id.Value,
            category.Name
        );
}
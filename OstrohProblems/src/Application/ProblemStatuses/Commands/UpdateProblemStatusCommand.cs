using Application.Common;
using Application.ProblemStatuses.Exceptions;
using Domain.ProblemStatuses;
using MediatR;

namespace Application.ProblemStatuses.Commands;

public record UpdateProblemStatusCommand : IRequest<Result<ProblemStatus, ProblemStatusException>>
{
    public required Guid ProblemStatusId { get; init; }
    public required string Name { get; init; }
}

public class UpdateManufacturerCommandHandler(
    IManufacturerRepository manufacturerRepository,
    ICategoryQueries categoryQueries)
    : IRequestHandler<UpdateProblemStatusCommand, Result<Manufacturer, ProblemStatusException>>
{
    public async Task<Result<Manufacturer, ProblemStatusException>> Handle(
        UpdateProblemStatusCommand request,
        CancellationToken cancellationToken)
    {
        var manufacturerId = new ManufacturerId(request.ManufacturerId);
        var existingManufacturer = await manufacturerRepository.GetById(manufacturerId, cancellationToken);

        var categoryList = new List<Category>();
        foreach (var categoryId in request.Categories)
        {
            var existingCategory = await categoryQueries.GetById(new CategoryId(categoryId), cancellationToken, false);

            var categoryResult = await existingCategory.Match<Task<Result<Category, ProblemStatusException>>>(
                async c =>
                {
                    categoryList.Add(c);
                    return c;
                },
                () => Task.FromResult<Result<Category, ProblemStatusException>>(
                    new CategoryNotFoundException(new CategoryId(categoryId)))
            );

            if (categoryResult.IsError)
            {
                return new ProblemStatusUnknownException(ManufacturerId.Empty, new Exception("Error with update manufacturer"));;
            }
        }

        return await existingManufacturer.Match<Task<Result<Manufacturer, ProblemStatusException>>>(
            async manufacturer => await UpdateManufacturer(manufacturer, request.Name, categoryList, cancellationToken),
            () => Task.FromResult<Result<Manufacturer, ProblemStatusException>>(
                new ProblemStatusNotFoundException(manufacturerId))
        );
    }

    private async Task<Result<Manufacturer, ProblemStatusException>> UpdateManufacturer(
        Manufacturer manufacturer,
        string name,
        List<Category> categories,
        CancellationToken cancellationToken)
    {
        try
        {
            manufacturer.UpdateName(name);
            manufacturer.SetCategories(categories);

            return await manufacturerRepository.Update(manufacturer, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemStatusUnknownException(manufacturer.Id, exception);
        }
    }
}

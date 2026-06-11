// src/CleanPOS.Application/Categories/Queries/GetAllCategories/GetAllCategoriesHandler.cs
namespace CleanPOS.Application.Categories.Queries.GetAllCategories;

using CleanPOS.Application.Categories.DTOs;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetAllCategoriesHandler
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(
        GetAllCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories
            .GetAllAsync(cancellationToken);

        return categories.Select(c => new CategoryDto(
            c.Id,
            c.Name,
            c.Description,
            c.Products.Count(p => !p.IsDeleted),
            c.CreatedAt
        ));
    }
}
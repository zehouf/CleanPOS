// src/CleanPOS.Application/Categories/Queries/GetCategoryById/GetCategoryByIdHandler.cs
namespace CleanPOS.Application.Categories.Queries.GetCategoryById;

using CleanPOS.Application.Categories.DTOs;
using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetCategoryByIdHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories
            .GetByIdAsync(query.Id, cancellationToken);

        if (category == null)
            throw new NotFoundException("Category", query.Id);

        return new CategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.Products.Count(p => !p.IsDeleted),
            category.CreatedAt
        );
    }
}
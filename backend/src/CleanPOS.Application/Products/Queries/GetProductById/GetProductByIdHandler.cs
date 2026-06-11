// src/CleanPOS.Application/Products/Queries/GetProductById/GetProductByIdHandler.cs
namespace CleanPOS.Application.Products.Queries.GetProductById;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Application.Products.DTOs;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetProductByIdHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products
            .GetByIdAsync(query.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException("Product", query.Id);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price,
            product.Stock,
            product.CategoryId,
            product.Category?.Name ?? string.Empty,
            product.CreatedAt
        );
    }
}
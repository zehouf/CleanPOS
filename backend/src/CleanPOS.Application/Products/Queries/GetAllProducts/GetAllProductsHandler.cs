// src/CleanPOS.Application/Products/Queries/GetAllProducts/GetAllProductsHandler.cs
namespace CleanPOS.Application.Products.Queries.GetAllProducts;

using CleanPOS.Application.Products.DTOs;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class GetAllProductsHandler
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDto>> Handle(
        GetAllProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products
            .GetAllAsync(cancellationToken);

        return products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Price,
            p.Stock,
            p.CategoryId,
            p.Category?.Name ?? string.Empty,
            p.CreatedAt
        ));
    }
}
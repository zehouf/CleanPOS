// src/CleanPOS.Application/Products/Queries/GetAllProducts/GetAllProductsQuery.cs
namespace CleanPOS.Application.Products.Queries.GetAllProducts;

using CleanPOS.Application.Products.DTOs;
using MediatR;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
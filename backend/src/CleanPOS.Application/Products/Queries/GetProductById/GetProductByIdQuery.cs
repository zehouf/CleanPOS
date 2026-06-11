// src/CleanPOS.Application/Products/Queries/GetProductById/GetProductByIdQuery.cs
namespace CleanPOS.Application.Products.Queries.GetProductById;

using CleanPOS.Application.Products.DTOs;
using MediatR;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
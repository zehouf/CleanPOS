// src/CleanPOS.Application/Products/Commands/CreateProduct/CreateProductCommand.cs
namespace CleanPOS.Application.Products.Commands.CreateProduct;

using MediatR;

public record CreateProductCommand(
    string Name,
    decimal Price,
    int Stock,
    Guid CategoryId
) : IRequest<Guid>;
// src/CleanPOS.Application/Products/Commands/UpdateProduct/UpdateProductCommand.cs
namespace CleanPOS.Application.Products.Commands.UpdateProduct;

using MediatR;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    Guid CategoryId
) : IRequest;
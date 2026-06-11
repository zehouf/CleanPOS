// src/CleanPOS.Application/Products/Commands/DeleteProduct/DeleteProductCommand.cs
namespace CleanPOS.Application.Products.Commands.DeleteProduct;

using MediatR;

public record DeleteProductCommand(Guid Id) : IRequest;
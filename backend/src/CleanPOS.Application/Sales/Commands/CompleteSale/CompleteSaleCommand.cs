// src/CleanPOS.Application/Sales/Commands/CompleteSale/CompleteSaleCommand.cs
namespace CleanPOS.Application.Sales.Commands.CompleteSale;

using MediatR;

public record CompleteSaleCommand(Guid SaleId) : IRequest;
// src/CleanPOS.Application/Sales/Commands/CancelSale/CancelSaleCommand.cs
namespace CleanPOS.Application.Sales.Commands.CancelSale;

using MediatR;

public record CancelSaleCommand(Guid SaleId) : IRequest;
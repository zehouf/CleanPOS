// src/CleanPOS.Application/Sales/Commands/AddSaleLine/AddSaleLineCommand.cs
namespace CleanPOS.Application.Sales.Commands.AddSaleLine;

using MediatR;

public record AddSaleLineCommand(
    Guid SaleId,
    Guid ProductId,
    int Quantity
) : IRequest<Guid>;
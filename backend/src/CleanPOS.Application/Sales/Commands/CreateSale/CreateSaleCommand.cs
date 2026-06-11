// src/CleanPOS.Application/Sales/Commands/CreateSale/CreateSaleCommand.cs
namespace CleanPOS.Application.Sales.Commands.CreateSale;

using MediatR;

public record CreateSaleCommand(
    string CashierId
) : IRequest<Guid>;
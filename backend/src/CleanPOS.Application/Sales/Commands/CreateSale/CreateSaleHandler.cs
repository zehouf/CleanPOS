// src/CleanPOS.Application/Sales/Commands/CreateSale/CreateSaleHandler.cs
namespace CleanPOS.Application.Sales.Commands.CreateSale;

using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class CreateSaleHandler
    : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateSaleCommand command,
        CancellationToken cancellationToken)
    {
        var sale = Sale.Create(command.CashierId);

        await _unitOfWork.Sales.AddAsync(sale, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return sale.Id;
    }
}
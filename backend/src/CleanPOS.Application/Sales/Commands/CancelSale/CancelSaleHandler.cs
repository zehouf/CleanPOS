// src/CleanPOS.Application/Sales/Commands/CancelSale/CancelSaleHandler.cs
namespace CleanPOS.Application.Sales.Commands.CancelSale;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class CancelSaleHandler
    : IRequestHandler<CancelSaleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelSaleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        CancelSaleCommand command,
        CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales
            .GetByIdAsync(command.SaleId, cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale", command.SaleId);

        // Domain — vérifie que la vente peut être annulée
        sale.Cancel();

        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
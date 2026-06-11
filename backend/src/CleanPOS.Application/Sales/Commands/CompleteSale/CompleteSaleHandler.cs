// src/CleanPOS.Application/Sales/Commands/CompleteSale/CompleteSaleHandler.cs
namespace CleanPOS.Application.Sales.Commands.CompleteSale;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class CompleteSaleHandler
    : IRequestHandler<CompleteSaleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CompleteSaleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        CompleteSaleCommand command,
        CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales
            .GetByIdAsync(command.SaleId, cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale", command.SaleId);

        // Domain — vérifie qu'il y a des lignes et finalise
        sale.Complete();

        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
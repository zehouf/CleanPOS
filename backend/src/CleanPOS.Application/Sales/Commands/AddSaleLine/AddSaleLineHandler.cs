// src/CleanPOS.Application/Sales/Commands/AddSaleLine/AddSaleLineHandler.cs
namespace CleanPOS.Application.Sales.Commands.AddSaleLine;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class AddSaleLineHandler
    : IRequestHandler<AddSaleLineCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddSaleLineHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        AddSaleLineCommand command,
        CancellationToken cancellationToken)
    {
        // Vérifier que la vente existe et est en Draft
        var sale = await _unitOfWork.Sales
            .GetByIdAsync(command.SaleId, cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale", command.SaleId);

        // Vérifier que le produit existe
        var product = await _unitOfWork.Products
            .GetByIdAsync(command.ProductId, cancellationToken);

        if (product == null)
            throw new NotFoundException("Product", command.ProductId);

        // Déduire le stock — règle métier dans le Domain
        product.DeductStock(command.Quantity);

        // Créer la ligne avec le prix actuel du produit
        var line = SaleLine.Create(
            command.SaleId,
            command.ProductId,
            command.Quantity,
            product.Price);

        // Ajouter la ligne à la vente — règle métier dans le Domain
        sale.AddLine(line);

        // Persister
        _unitOfWork.Products.Update(product);
        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return line.Id;
    }
}
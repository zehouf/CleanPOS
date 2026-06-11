// src/CleanPOS.Application/Products/Commands/UpdateProduct/UpdateProductHandler.cs
namespace CleanPOS.Application.Products.Commands.UpdateProduct;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class UpdateProductHandler
    : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products
            .GetByIdAsync(command.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException("Product", command.Id);

        // Vérifier que la nouvelle catégorie existe
        var category = await _unitOfWork.Categories
            .GetByIdAsync(command.CategoryId, cancellationToken);

        if (category == null)
            throw new NotFoundException("Category", command.CategoryId);

        // Vérifier unicité du nom seulement si changé
        if (!product.Name.Equals(command.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _unitOfWork.Products
                .ExistsByNameAsync(command.Name, cancellationToken);

            if (exists)
                throw new DuplicateNameException("Product", command.Name);
        }

        // Domain — modifier l'objet
        product.Update(
            command.Name,
            command.Price,
            command.Stock,
            command.CategoryId);

        // Repository — persister
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
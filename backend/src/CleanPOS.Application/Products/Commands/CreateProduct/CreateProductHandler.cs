// src/CleanPOS.Application/Products/Commands/CreateProduct/CreateProductHandler.cs
namespace CleanPOS.Application.Products.Commands.CreateProduct;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class CreateProductHandler
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        // Vérifier que la catégorie existe
        var category = await _unitOfWork.Categories
            .GetByIdAsync(command.CategoryId, cancellationToken);

        if (category == null)
            throw new NotFoundException("Category", command.CategoryId);

        // Vérifier l'unicité du nom
        var exists = await _unitOfWork.Products
            .ExistsByNameAsync(command.Name, cancellationToken);

        if (exists)
            throw new DuplicateNameException("Product", command.Name);

        // Domain — créer l'objet valide
        var product = Product.Create(
            command.Name,
            command.Price,
            command.Stock,
            command.CategoryId);

        // Repository — persister
        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
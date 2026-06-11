// src/CleanPOS.Application/Products/Commands/DeleteProduct/DeleteProductHandler.cs
namespace CleanPOS.Application.Products.Commands.DeleteProduct;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class DeleteProductHandler
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products
            .GetByIdAsync(command.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException("Product", command.Id);

        product.Delete();

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
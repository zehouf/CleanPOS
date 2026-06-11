// src/CleanPOS.Application/Categories/Commands/DeleteCategory/DeleteCategoryHandler.cs
namespace CleanPOS.Application.Categories.Commands.DeleteCategory;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class DeleteCategoryHandler
    : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DeleteCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories
            .GetByIdAsync(command.Id, cancellationToken);

        if (category == null)
            throw new NotFoundException("Category", command.Id);

        // Domain — suppression logique
        category.Delete();

        // Repository — persister
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
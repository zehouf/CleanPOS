// src/CleanPOS.Application/Categories/Commands/UpdateCategory/UpdateCategoryHandler.cs
namespace CleanPOS.Application.Categories.Commands.UpdateCategory;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class UpdateCategoryHandler
    : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories
            .GetByIdAsync(command.Id, cancellationToken);

        if (category == null)
            throw new NotFoundException("Category", command.Id);

        // Vérifier unicité du nouveau nom
        // seulement si le nom a changé
        if (!category.Name.Equals(command.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _unitOfWork.Categories
                .ExistsByNameAsync(command.Name, cancellationToken);

            if (exists)
                throw new DuplicateNameException("Category", command.Name);
        }

        // Domain — modifier l'objet
        category.Update(command.Name, command.Description);

        // Repository — persister
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
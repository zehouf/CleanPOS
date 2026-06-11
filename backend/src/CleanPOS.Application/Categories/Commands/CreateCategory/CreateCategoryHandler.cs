// src/CleanPOS.Application/Categories/Commands/CreateCategory/CreateCategoryHandler.cs
namespace CleanPOS.Application.Categories.Commands.CreateCategory;

using CleanPOS.Application.Common.Exceptions;
using CleanPOS.Domain.Entities;
using CleanPOS.Domain.Interfaces;
using MediatR;

public class CreateCategoryHandler
    : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        // Règle métier — vérifier l'unicité du nom
        var exists = await _unitOfWork.Categories
            .ExistsByNameAsync(command.Name, cancellationToken);

        if (exists)
            throw new DuplicateNameException("Category", command.Name);

        // Domain — créer l'objet valide
        var category = Category.Create(command.Name, command.Description);

        // Repository — persister
        await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
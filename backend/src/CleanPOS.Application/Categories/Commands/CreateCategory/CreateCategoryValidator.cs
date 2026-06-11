// src/CleanPOS.Application/Categories/Commands/CreateCategory/CreateCategoryValidator.cs
namespace CleanPOS.Application.Categories.Commands.CreateCategory;

using FluentValidation;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
            .MaximumLength(200)
                .WithMessage("Le nom ne peut pas dépasser 200 caractères.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
                .WithMessage("La description ne peut pas dépasser 500 caractères.")
            .When(x => x.Description != null);
    }
}
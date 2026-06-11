// src/CleanPOS.Application/Categories/Commands/UpdateCategory/UpdateCategoryValidator.cs
namespace CleanPOS.Application.Categories.Commands.UpdateCategory;

using FluentValidation;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("L'identifiant est obligatoire.");

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
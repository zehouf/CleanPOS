// src/CleanPOS.Application/Products/Commands/UpdateProduct/UpdateProductValidator.cs
namespace CleanPOS.Application.Products.Commands.UpdateProduct;

using FluentValidation;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("L'identifiant est obligatoire.");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
            .MaximumLength(200)
                .WithMessage("Le nom ne peut pas dépasser 200 caractères.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Le prix ne peut pas être négatif.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Le stock ne peut pas être négatif.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
                .WithMessage("La catégorie est obligatoire.");
    }
}
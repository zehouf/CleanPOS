// src/CleanPOS.Application/Products/Commands/CreateProduct/CreateProductValidator.cs
namespace CleanPOS.Application.Products.Commands.CreateProduct;

using FluentValidation;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
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
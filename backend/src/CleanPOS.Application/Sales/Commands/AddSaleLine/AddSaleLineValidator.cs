// src/CleanPOS.Application/Sales/Commands/AddSaleLine/AddSaleLineValidator.cs
namespace CleanPOS.Application.Sales.Commands.AddSaleLine;

using FluentValidation;

public class AddSaleLineValidator : AbstractValidator<AddSaleLineCommand>
{
    public AddSaleLineValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
                .WithMessage("La vente est obligatoire.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage("Le produit est obligatoire.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .WithMessage("La quantité doit être supérieure à 0.");
    }
}
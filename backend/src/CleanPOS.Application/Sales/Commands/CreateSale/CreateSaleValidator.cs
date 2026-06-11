// src/CleanPOS.Application/Sales/Commands/CreateSale/CreateSaleValidator.cs
namespace CleanPOS.Application.Sales.Commands.CreateSale;

using FluentValidation;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.CashierId)
            .NotEmpty()
                .WithMessage("Le caissier est obligatoire.");
    }
}
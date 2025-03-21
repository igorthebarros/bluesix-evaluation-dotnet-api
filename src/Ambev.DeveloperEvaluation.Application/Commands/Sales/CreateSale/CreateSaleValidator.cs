using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.ProductsTotalAmount).GreaterThan(uint.MinValue);
            RuleFor(sale => sale.ItemsPurchased).Must(sale => sale.All(saleItem => saleItem.Quantity <= 20))
                .WithMessage("No item can have a quantity greater than 20.");
        }
    }
}

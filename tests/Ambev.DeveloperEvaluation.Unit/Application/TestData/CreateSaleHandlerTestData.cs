using Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateSaleHandlerTestData
    {
        private static readonly Faker<Product> productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => f.Random.Float(10, 500))
                .RuleFor(p => p.IsActive, f => f.Random.Bool());

        private static readonly Faker<SaleItem> saleItemFaker = new Faker<SaleItem>()
                .RuleFor(si => si.SaleId, f => Guid.NewGuid())
                .RuleFor(si => si.ProductId, f => Guid.NewGuid())
                .RuleFor(si => si.ProductName, f => f.Commerce.ProductName())
                .RuleFor(si => si.Product, f => productFaker.Generate())
                .RuleFor(si => si.Quantity, f => f.Random.UInt(1, 10))
                .RuleFor(si => si.Price, (f, si) => si.Product.Price)
                .RuleFor(si => si.IsDiscountAvailable, f => f.Random.Bool())
                .RuleFor(si => si.WasDiscountApplied, (f, si) => si.IsDiscountAvailable && f.Random.Bool())
                .RuleFor(si => si.AmountOfDiscountApplied, (f, si) => si.WasDiscountApplied ? f.Random.Float(1, si.Price * 0.2f) : 0);

        private static readonly Faker<CreateSaleCommand> createSaleHandlerFake = new Faker<CreateSaleCommand>()
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
                .RuleFor(s => s.UpdatedAt, (f, s) => f.Random.Bool() ? f.Date.Recent(30) : null)
                .RuleFor(s => s.CustomerId, f => Guid.NewGuid())
                .RuleFor(s => s.Branch, f => f.Company.CompanyName())
                .RuleFor(s => s.ProductsTotalAmount, f => f.Random.UInt(1, 20))
                .RuleFor(s => s.IsCanceled, f => f.Random.Bool(0.1f)) // 10% chance of being canceled
                .RuleFor(s => s.ItemsPurchased, f => saleItemFaker.Generate(f.Random.Int(1, 5))) // Generates 1 to 5 items
                .RuleFor(s => s.PriceTotalAmount, (f, s) => s.ItemsPurchased.Sum(i => i.Price * i.Quantity - i.AmountOfDiscountApplied));

        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleHandlerFake.Generate();
        }
    }
}

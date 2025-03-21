using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public required Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public required Product Product { get; set; }
        public uint Quantity { get; set; }
        public float Price { get; set; }
        public bool IsDiscountAvailable { get; set; } = false;
        public bool WasDiscountApplied { get; set; } = false;
        public float AmountOfDiscountApplied { get; set; } = 0;

    }
}

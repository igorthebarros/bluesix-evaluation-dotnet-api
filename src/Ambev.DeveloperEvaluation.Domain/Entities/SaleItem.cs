using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public uint ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public required Product Product { get; set; }
        public uint Quantity { get; set; }
        // This property reflects the item's price at the purchase moment
        public float Price { get; set; }
        public bool IsDiscountAvailable { get; set; } = false;
        public bool WasDiscountApplied { get; set; } = false;
        public float AmountOfDiscountApplied { get; set; } = 0;

    }
}

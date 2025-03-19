using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public required uint SaleNumber { get; set; }
        public required User Customer { get; set; }
        public float TotalAmount { get; private set; }
        public string Branch { get; private set; } = string.Empty;
        public required IEnumerable<SaleItem> ItemsPurchased { get; set; }
        public uint ProductsTotalAmount { get; private set; }
        public bool IsCanceled { get; private set; }

        public Sale()
        {
            IsCanceled = false;
        }

        public void GetProductsTotalAmount()
        {
            TotalAmount = ItemsPurchased.Sum(x => x.ProductPriceAtPurchase);
        }

    }

}
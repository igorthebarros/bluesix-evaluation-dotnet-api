using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Sale()
        {
            IsCanceled = false;
        }

        public required Guid CustomerId { get; set; }
        public float PriceTotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public uint ProductsTotalAmount { get; set; }
        public bool IsCanceled { get; set; }

        // Not a navigation property. Used only for API purposes
        public IEnumerable<SaleItem> ItemsPurchased { get; set; }

        public void GetProductsTotalAmount(IEnumerable<SaleItem>? saleItems)
        {
            if (saleItems == null)
                PriceTotalAmount = ItemsPurchased.Sum(x => x.Price);
            else
                PriceTotalAmount = saleItems.Where(x => x.SaleId == Id).Sum(y => y.Price);
        }
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService : IBaseService<Sale>
    {
        void CheckForDiscountBasedIdenticalItems(SaleItem item);
        bool IsItemQuantityExceedingLimits(uint quantity);
        SaleItem ApplyDiscountOnSaleItem(float amountOfDiscount, SaleItem item);
    }
}

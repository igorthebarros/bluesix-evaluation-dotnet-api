using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleItemService : IBaseService<SaleItem>
    {
        bool IsItemElegibleToDiscount(SaleItem item);
        SaleItem ApplyDiscount(SaleItem item);
    }
}

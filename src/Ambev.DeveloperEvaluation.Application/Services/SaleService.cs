using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IBaseRespository<Sale> _repository;
        public SaleService(IBaseRespository<Sale> repository)
        {
            _repository = repository;
        }

        public void CheckForDiscountBasedIdenticalItems(SaleItem item)
        {
            var exceeding = IsItemQuantityExceedingLimits(item.Quantity);

            if (exceeding)
                throw new Exception($"Exceeded maximum amount of units for item: {item.ProductName}");

            if (item.Quantity < 4)
                return;

            if (item.Quantity > 4 && item.Quantity < 10)
            {
                item.IsDiscountAvailable = true;
                item.AmountOfDiscountApplied = 10;
            }

            if (item.Quantity >= 10  && item.Quantity < 20)
            {
                item.IsDiscountAvailable = true;
                item.AmountOfDiscountApplied = 20;
            }

            ApplyDiscountOnSaleItem(item.AmountOfDiscountApplied, item);
        }

        public bool IsItemQuantityExceedingLimits(uint quantity)
        {
            if (quantity > 20)
                return true;

            return false;
        }

        public SaleItem ApplyDiscountOnSaleItem(float amountOfDiscount, SaleItem item)
        {
            item.Price = (item.Price * amountOfDiscount) / 100;
            return item;
        }

        public async Task<Sale> CreateAsync(Sale entity, CancellationToken cancellationToken = default)
        {
            foreach (var item in entity.ItemsPurchased)
                CheckForDiscountBasedIdenticalItems(item);

            var result = await _repository.CreateAsync (entity, cancellationToken).ConfigureAwait(false);

            return result;

        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Sale> UpdateAsync(Sale entity, CancellationToken cancellationToken = default)
        {
            return await _repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
        }
    }
}

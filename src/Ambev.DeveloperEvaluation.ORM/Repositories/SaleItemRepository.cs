using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of IBaseRespository<SaleItem> using Entity Framework Core
    /// </summary>
    public class SaleItemRepository : IBaseRespository<SaleItem>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleItemRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Creates a new saleItem in the database
        /// </summary>
        /// <param name="item">A item from a sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(item, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return item;
        }


        /// <summary>
        /// Deletes a saleItem from the database
        /// </summary>
        /// <param name="id">The unique identifier of the saleItem to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the saleItem was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            if (saleItem == null)
                return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Retrieves all saleItems from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of immutable saleItems</returns>
        public async Task<IReadOnlyList<SaleItem>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var saleItems = await _context.SaleItems.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (saleItems == null) 
                return null;

            return saleItems;
        }

        /// <summary>
        /// Retrieve a saleItem by it's Guid Id from the database
        /// </summary>
        /// <param name="id">The unique identifier of the sale to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A saleItem if it's found or, if not, a default object</returns>
        public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var saleItem = await _context.SaleItems.FirstOrDefaultAsync(x => x.Id.Equals(id)).ConfigureAwait(false);

            return saleItem;
        }

        /// <summary>
        /// Update a sale register replacing it by the new register. This changes all the entity.
        /// </summary>
        /// <param name="saleItem">The new sale entity to be registered</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Creates a new saleItem, if no equally saleItem is found beforehand,
        /// otherwise, update the existant saleItem entity by the new one</returns>
        public async Task<SaleItem> UpdateAsync(SaleItem saleItemNew, CancellationToken cancellationToken = default)
        {
            var saleItemExistant = await GetByIdAsync(saleItemNew.Id, cancellationToken).ConfigureAwait(false);
            if (saleItemExistant == null)
                await _context.SaleItems.AddAsync(saleItemNew, cancellationToken).ConfigureAwait(false);
            else
                _context.SaleItems.Update(saleItemNew);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return saleItemNew;
        }
    }
}

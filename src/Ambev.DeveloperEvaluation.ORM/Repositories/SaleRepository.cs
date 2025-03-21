using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of IBaseRespository<Sale> using Entity Framework Core
    /// </summary>
    public class SaleRepository : IBaseRespository<Sale>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the database
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return sale;
        }

        /// <summary>
        /// Deletes a sale from the database
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Retrieves all sales from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of immutable sales</returns>
        public async Task<IReadOnlyList<Sale>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sales = await _context.Sales.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (sales == null)
                return null;

            return sales;
        }

        /// <summary>
        /// Retrieve a sale by it's Guid Id from the database
        /// </summary>
        /// <param name="id">The unique identifier of the sale to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A sale if it's found or, if not, a default object</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id.Equals(id)).ConfigureAwait(false);

            return sale;
        }

        /// <summary>
        /// Update a sale register replacing it by the new register. This changes all the entity.
        /// </summary>
        /// <param name="sale">The new sale entity to be registered</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Creates a new sale, if no equally sale is found beforehand,
        /// otherwise, update the existant sale entity by the new one</returns>
        public async Task<Sale> UpdateAsync(Sale saleNew, CancellationToken cancellationToken = default)
        {
            var saleExistant = await GetByIdAsync(saleNew.Id, cancellationToken).ConfigureAwait(false);
            if (saleExistant == null)
                await _context.Sales.AddAsync(saleNew, cancellationToken).ConfigureAwait(false);
            else 
                _context.Sales.Update(saleNew);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return saleNew;
        }
    }
}

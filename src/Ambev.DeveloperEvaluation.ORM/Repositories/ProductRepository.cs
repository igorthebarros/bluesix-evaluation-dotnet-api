using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of IBaseRespository<Product> using Entity Framework Core
    /// </summary>
    public class ProductRepository : IBaseRespository<Product>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of ProductRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new product in the database
        /// </summary>
        /// <param name="product">A product and it's details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return product;
        }


        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the product was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Retrieves all products from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of immutable products</returns>
        public async Task<IReadOnlyList<Product>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (products == null)
                return null;

            return products;
        }

        /// <summary>
        /// Retrieve a product by it's Guid Id from the database
        /// </summary>
        /// <param name="id">The unique identifier of the product to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A product if it's found or, if not, a default object</returns>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id.Equals(id)).ConfigureAwait(false);

            return product;
        }

        /// <summary>
        /// Update a sale register replacing it by the new register. This changes all the entity.
        /// </summary>
        /// <param name="product">The new sale entity to be registered</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Creates a new product, if no equally product is found beforehand,
        /// otherwise, update the existant product entity by the new one</returns>
        public async Task<Product> UpdateAsync(Product productNew, CancellationToken cancellationToken = default)
        {
            var productExistant = await GetByIdAsync(productNew.Id, cancellationToken).ConfigureAwait(false);
            if (productExistant == null)
                await _context.Products.AddAsync(productNew, cancellationToken).ConfigureAwait(false);
            else
                _context.Products.Update(productNew);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return productNew;
        }
    }
}

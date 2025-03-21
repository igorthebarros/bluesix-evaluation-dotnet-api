using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.DeleteSale
{
    /// <summary>
    /// Command for deleting a sale
    /// </summary>
    public record DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        /// <summary>
        /// The unique identifier of the sale to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of DeleteSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to delete</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}

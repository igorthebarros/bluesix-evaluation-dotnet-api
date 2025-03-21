using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.GetSale
{
    /// <summary>
    /// Command for retrieving a sale by its id
    /// </summary>
    public record GetSaleCommand : IRequest<GetSaleResult>
    {
        public Guid Id { get; set; }

        public GetSaleCommand()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="id">The Id of the sale to retrieve</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}

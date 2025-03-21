using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing DeleteSaleCommand requests
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleService _service;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        /// <param name="service">The sale service layer</param>
        public DeleteSaleHandler(ISaleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request
        /// </summary>
        /// <param name="request">The DeleteSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _service.DeleteAsync(command.Id, cancellationToken).ConfigureAwait(false);
            if (!success)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            return new DeleteSaleResponse { Success = true };

        }
    }
}

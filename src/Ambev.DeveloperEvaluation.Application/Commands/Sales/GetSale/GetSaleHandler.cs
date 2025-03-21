using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.GetSale
{
    /// <summary>
    /// Handler for processing GetSaleCommand requests
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleService _service;
        private readonly IMapper _mapper;


        /// <summary>
        /// Initializes a new instance of GetSaleHandler
        /// </summary>
        /// <param name="service">The sale service layer</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetSaleHandler(ISaleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _service.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with Id {command.Id} not found");

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}

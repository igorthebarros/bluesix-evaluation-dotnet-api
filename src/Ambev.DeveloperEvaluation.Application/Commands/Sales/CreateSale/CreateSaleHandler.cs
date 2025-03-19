using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand,  CreateSaleResult>
    {
        private readonly IBaseRespository<Sale> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateUserHandler
        /// </summary>
        /// <param name="repository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateSaleCommand</param>
        public CreateSaleHandler(IBaseRespository<Sale> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand  command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(command);

            var createdSale = await _repository.CreateAsync(sale, cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}

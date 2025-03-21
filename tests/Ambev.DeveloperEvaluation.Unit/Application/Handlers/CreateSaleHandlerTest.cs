using Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Handlers
{
    public class CreateSaleHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ISaleService _service;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTest()
        {
            _mapper = Substitute.For<IMapper>();
            _service = Substitute.For<ISaleService>();
            _handler = new CreateSaleHandler(_mapper, _service);
        }

        /// <summary>
        /// Test an all valid sale command if the request is being handled correctly.
        /// </summary>
        [Fact(DisplayName = "Given a valid sale data request When creating a new sale Then return a success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale
            {
                CustomerId = command.CustomerId,
                Branch = command.Branch,
                CreatedAt = command.CreatedAt,
                Id = command.Id,
                IsCanceled = command.IsCanceled,
                ItemsPurchased = command.ItemsPurchased,
                PriceTotalAmount = command.PriceTotalAmount,
                ProductsTotalAmount = command.ProductsTotalAmount,
                UpdatedAt = command.UpdatedAt

            };
            var result = new CreateSaleResult
            {
                Id = command.Id,
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);

            _service.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            var createdSaleResult = await _handler.Handle(command, CancellationToken.None);

            createdSaleResult.Should().NotBeNull();
            createdSaleResult.Id.Should().Be(command.Id);
            await _service.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }
    }
}

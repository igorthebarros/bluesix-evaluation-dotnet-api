using Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Commands.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Commands.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    [Route("api/v1/sale")]
    [ApiController]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new sale
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            // Improve method adding different resposnse types 
            var result = await _mediator.Send(command).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Get a sale by Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromHeader] GetSaleCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);

            if (result == null)
                return NotFound(new { message = "Sale not found" });

            return Ok(result);
        }
        /// <summary>
        /// Delete a sale
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(DeleteSaleCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);

            if (result == null)
                return NotFound(new { message = "Sale not found" });

            return NoContent();

        }
    }
}

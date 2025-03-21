using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.DeleteSale
{
    /// <summary>
    /// Validator for DeleteSaleCommand
    /// </summary>
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for DeleteSaleCommand
        /// </summary>
        public DeleteSaleValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale Id is required");
        }
    }
}

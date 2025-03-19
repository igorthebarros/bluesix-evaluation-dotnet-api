using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and CreateSaleResponse
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale operation
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<Sale, CreateSaleResult>();
        }
    }
}

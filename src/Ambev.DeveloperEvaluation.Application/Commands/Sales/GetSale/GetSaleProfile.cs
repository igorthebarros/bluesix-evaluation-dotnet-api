using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between User entity and GetUserResponse
    /// </summary>
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile() 
        {
            CreateMap<Sale, GetSaleResult>();
        }
    }
}

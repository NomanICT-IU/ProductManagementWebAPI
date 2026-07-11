using AutoMapper;
using ProductManagementWebAPI.Data;
using ProductManagementWebAPI.Models;

namespace ProductManagementWebAPI.Automapper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();

            CreateMap<ProductModel, Product>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}

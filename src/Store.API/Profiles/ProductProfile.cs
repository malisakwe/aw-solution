using AutoMapper;
using Store.Application.Dtos.Product;
using Store.Domain.Entities;

namespace Store.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.SubCategoryName,
                           opt => opt.MapFrom(src => src.SubCategory!.Name));
        }
    }
}

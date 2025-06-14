using AutoMapper;
using Store.Application.Dtos.Category;
using Store.Application.Dtos.Product;
using Store.Domain.Entities;

namespace Store.Application.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            // Product -> ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory));

            // SubCategory -> SubCategoryDto
            CreateMap<SubCategory, SubCategoryDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // Category -> CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

            // Product -> CreateProductDto (if needed, usually for reverse mapping)
            CreateMap<Product, CreateProductDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ListPrice))
                .ReverseMap()
                .ForMember(dest => dest.ListPrice, opt => opt.MapFrom(src => src.Price));
        }
    }
}


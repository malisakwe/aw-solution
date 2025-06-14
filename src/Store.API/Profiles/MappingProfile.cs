using AutoMapper;
using Store.Application.Dtos.Category;
using Store.Application.Dtos.Product;
using Store.Domain.Entities;

namespace Store.API.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>(); 
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<SubCategoryDto, SubCategory>();
        }
    }
}

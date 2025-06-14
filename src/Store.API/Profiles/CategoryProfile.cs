using AutoMapper;
using Store.Application.Dtos.Category;
using Store.Domain.Entities;

namespace Store.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>();

            // SubCategory mappings
            CreateMap<SubCategory, SubCategoryDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}

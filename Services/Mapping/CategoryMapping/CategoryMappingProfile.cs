using AutoMapper;
using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Services.DTOs.Categories;
using NLayerArchitectureV2.Services.DTOs.Categories.Create;
using NLayerArchitectureV2.Services.DTOs.Categories.Update;

namespace NLayerArchitectureV2.Services.Mapping.CategoryMapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
            CreateMap<UpdateCategoryRequest, Category>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
        }
    }
}

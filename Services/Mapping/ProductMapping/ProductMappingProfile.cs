using AutoMapper;
using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Services.DTOs.Products;
using NLayerArchitectureV2.Services.DTOs.Products.Create;
using NLayerArchitectureV2.Services.DTOs.Products.Update;

namespace NLayerArchitectureV2.Services.Mapping.ProductMapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
            CreateMap<UpdateProductRequest, Product>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
        }
    }
}

namespace NLayerArchitectureV2.Services.DTOs.Products
{
    public record ProductDto(int Id, string Name, decimal Price, int Stock,int CategoryId,DateTime Created, DateTime Updated);
}

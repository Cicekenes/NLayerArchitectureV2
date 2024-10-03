namespace NLayerArchitectureV2.Services.DTOs.Products.Create
{
    public record CreateProductRequest(string Name, decimal Price, int Stock,int CategoryId);
}

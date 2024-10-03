namespace NLayerArchitectureV2.Services.DTOs.Products.Update
{
    public record UpdateProductRequest(string Name, decimal Price, int Stock, int CategoryId);
}

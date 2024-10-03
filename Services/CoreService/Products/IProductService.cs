using NLayerArchitectureV2.Services.DTOs.Products;
using NLayerArchitectureV2.Services.DTOs.Products.Create;
using NLayerArchitectureV2.Services.DTOs.Products.Update;
using NLayerArchitectureV2.Services.ResultPattern;

namespace NLayerArchitectureV2.Services.CoreService.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult<List<ProductDto>>> GetAllAsync();
        Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> UpdateStock(UpdateProductStockRequest request);
    }
}

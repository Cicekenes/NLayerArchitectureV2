using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Services.DTOs.Categories;
using NLayerArchitectureV2.Services.DTOs.Categories.Create;
using NLayerArchitectureV2.Services.DTOs.Categories.Update;
using NLayerArchitectureV2.Services.ResultPattern;

namespace NLayerArchitectureV2.Services.CoreService.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        //Task<Category?> GetCategoryWithProductsAsync(int id);
        //IQueryable<Category?> GetCategoryByProductsAsync();
    }
}

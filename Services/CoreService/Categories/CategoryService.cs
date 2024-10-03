using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.CategoryRepositories;
using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Repositories.UnitOfWorks;
using NLayerArchitectureV2.Services.DTOs.Categories;
using NLayerArchitectureV2.Services.DTOs.Categories.Create;
using NLayerArchitectureV2.Services.DTOs.Categories.Update;
using NLayerArchitectureV2.Services.ResultPattern;
using System.Collections.Generic;
using System.Net;

namespace NLayerArchitectureV2.Services.CoreService.Categories
{
    public class CategoryService(ICategoryRepository _categoryRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : ICategoryService
    {
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId);

            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
            }

            var categoryAsDto = _mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await _categoryRepository.GetCategoryWithProducts().ToListAsync();

            var categoryAsDto = _mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();

            var categoriesAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
            }

            var categoryAsDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);

        }
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var hasCategory = await _categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (hasCategory)
            {
                return ServiceResult<int>.Fail("Bu isme sahip kategori bulunmaktadır", HttpStatusCode.BadRequest);
            }

            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(category.Id, $"api/categories/{category.Id}");
        }
        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ServiceResult.Fail("Kategori bulunamadı.", HttpStatusCode.NotFound);
            }

            var isCategoryNameExists = await _categoryRepository.Where(x => x.Name == request.Name && x.Id == category.Id).AnyAsync();

            if (isCategoryNameExists)
            {
                return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            category = _mapper.Map(request, category);

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ServiceResult.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
            }

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}

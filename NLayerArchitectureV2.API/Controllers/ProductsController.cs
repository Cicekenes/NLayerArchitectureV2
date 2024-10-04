using Microsoft.AspNetCore.Mvc;
using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Services.CoreService.Products;
using NLayerArchitectureV2.Services.DTOs.Products.Create;
using NLayerArchitectureV2.Services.DTOs.Products.Update;
using NLayerArchitectureV2.Services.Filters;

namespace NLayerArchitectureV2.API.Controllers
{
    public class ProductsController(IProductService _productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _productService.GetAllAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await _productService.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await _productService.CreateAsync(request));

        [ServiceFilter<NotFoundFilter<Product, int>>]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await _productService.UpdateAsync(id, request));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await _productService.UpdateStock(request));

        [ServiceFilter<NotFoundFilter<Product, int>>]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _productService.DeleteAsync(id));
    }
}

﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.ProductRepositories;
using NLayerArchitectureV2.Repositories.Entities;
using NLayerArchitectureV2.Repositories.UnitOfWorks;
using NLayerArchitectureV2.Services.DTOs.Products;
using NLayerArchitectureV2.Services.DTOs.Products.Create;
using NLayerArchitectureV2.Services.DTOs.Products.Update;
using NLayerArchitectureV2.Services.ResultPattern;
using System.Net;

namespace NLayerArchitectureV2.Services.CoreService.Products
{
    public class ProductService(IProductRepository _productRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await _productRepository.GetTopPriceProductsAsync(count);

            //var productsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productsAsDto = _mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>
            {
                Data = productsAsDto
            };
        }
        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            //throw new CriticalException("kritik seviye bir hata meydana geldi");
            var products = await _productRepository.GetAll().ToListAsync();

            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productsAsDto = _mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {

            var products = await _productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productsAsDto = _mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }
        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("Product is not found", HttpStatusCode.NotFound);
            }
            //var productsAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);

            var productsAsDto = _mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(productsAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            var anyProduct = await _productRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Böyle bir ürün bulunmaktadır", HttpStatusCode.BadRequest);
            }

            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
          
            var isProductNameExists = await _productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

            if (isProductNameExists)
            {
                return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            var product = _mapper.Map<Product>(request);
            product.Id = id;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStock(UpdateProductStockRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


    }
}
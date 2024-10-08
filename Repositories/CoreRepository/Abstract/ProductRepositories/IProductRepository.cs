﻿using NLayerArchitectureV2.Repositories.Entities;

namespace NLayerArchitectureV2.Repositories.CoreRepository.Abstract.ProductRepositories
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        Task<List<Product>> GetTopPriceProductsAsync(int count);
    }
}

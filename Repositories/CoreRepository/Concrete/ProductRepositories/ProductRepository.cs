using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.ProductRepositories;
using NLayerArchitectureV2.Repositories.Database;
using NLayerArchitectureV2.Repositories.Entities;

namespace NLayerArchitectureV2.Repositories.CoreRepository.Concrete.ProductRepositories
{
    public class ProductRepository(AppDbContext _dbContext) : GenericRepository<Product, int>(_dbContext), IProductRepository
    {
        public async Task<List<Product>> GetTopPriceProductsAsync(int count)
        {
            return await Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}

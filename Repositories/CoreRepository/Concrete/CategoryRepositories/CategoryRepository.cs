using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.CategoryRepositories;
using NLayerArchitectureV2.Repositories.Database;
using NLayerArchitectureV2.Repositories.Entities;

namespace NLayerArchitectureV2.Repositories.CoreRepository.Concrete.CategoryRepositories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public IQueryable<Category?> GetCategoryWithProducts()
        {
            return context.Categories.Include(x => x.Products).AsQueryable();
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return await context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

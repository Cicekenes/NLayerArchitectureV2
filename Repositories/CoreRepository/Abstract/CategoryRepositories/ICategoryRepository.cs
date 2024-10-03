using NLayerArchitectureV2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitectureV2.Repositories.CoreRepository.Abstract.CategoryRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        IQueryable<Category?> GetCategoryWithProducts();
    }
}

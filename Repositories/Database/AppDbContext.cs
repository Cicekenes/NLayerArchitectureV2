using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.Entities;
using System.Reflection;

namespace NLayerArchitectureV2.Repositories.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        #region DbSets
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Anlamı : Bu repositories katmanındaki tüm entitytypeconfigleri al uygula.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

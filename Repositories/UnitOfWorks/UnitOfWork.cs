using NLayerArchitectureV2.Repositories.Database;

namespace NLayerArchitectureV2.Repositories.UnitOfWorks
{
    public class UnitOfWork(AppDbContext Context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerArchitectureV2.Repositories.CoreRepository;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.CategoryRepositories;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.ProductRepositories;
using NLayerArchitectureV2.Repositories.CoreRepository.Concrete.CategoryRepositories;
using NLayerArchitectureV2.Repositories.CoreRepository.Concrete.ProductRepositories;
using NLayerArchitectureV2.Repositories.Database;
using NLayerArchitectureV2.Repositories.Interceptors;
using NLayerArchitectureV2.Repositories.OptionsPattern;
using NLayerArchitectureV2.Repositories.RepositoryAssemblies;
using NLayerArchitectureV2.Repositories.UnitOfWorks;

namespace NLayerArchitectureV2.Repositories.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositoriesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                // IOptions<ConnectionStrings> servisini al ve kullan
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                    sqlServerOptionsAction.EnableRetryOnFailure();
                });
                options.AddInterceptors(new AuditDbContextInterceptor());
            });

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}

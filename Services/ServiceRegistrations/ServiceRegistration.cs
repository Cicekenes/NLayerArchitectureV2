using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerArchitectureV2.Services.CoreService.Categories;
using NLayerArchitectureV2.Services.CoreService.Products;
using NLayerArchitectureV2.Services.ExceptionHandlers;
using NLayerArchitectureV2.Services.Filters;
using System.Reflection;

namespace NLayerArchitectureV2.Services.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(NotFoundFilter<,>));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}

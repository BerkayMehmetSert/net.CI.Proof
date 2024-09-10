using Api.Application.Service;
using Api.Infrastructure;
using Api.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class ServiceConfigurations
{
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
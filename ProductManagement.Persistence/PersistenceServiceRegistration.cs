using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Contracts;
using ProductManagement.Persistence.DatabaseContext;
using ProductManagement.Persistence.Repositories;

namespace ProductManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        var teste = configuration.GetConnectionString("ProductManagementConnectionString");
        services.AddDbContext<ProductManagementContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("ProductManagementConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}

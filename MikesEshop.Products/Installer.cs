using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikesEshop.Products.Infrastructure;
using MikesEshop.Products.Infrastructure.Services;
using MikesEshop.Shared.Application.Services;

namespace MikesEshop.Products;

public static class Installer
{
    public static IServiceCollection AddProducts(this IServiceCollection services, string connectionString)
    {
        return services
            .AddTransient(typeof(IRepository<>), typeof(EfCoreRepository<>))
            .AddTransient(typeof(IQueryObject<>), typeof(EfCoreQueryObject<>))
            .AddDbContext<ProductsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
    }
}
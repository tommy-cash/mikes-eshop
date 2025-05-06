using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikesEshop.Products.Infrastructure;

namespace MikesEshop.Products;

public static class Installer
{
    public static IServiceCollection AddProducts(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}
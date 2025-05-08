using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MikesEshop.Host;
using MikesEshop.Host.Middlewares;
using MikesEshop.Products;
using MikesEshop.Products.Infrastructure;
using MikesEshop.Products.Infrastructure.Seeds;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var assemblies = builder.Configuration.GetSection("WolverineEndpointAssemblies").Get<string[]>() ?? [];
var eshopProductsDbConnectionString = builder.Configuration.GetConnectionString("EshopProductsDb");

Guard.Against.NullOrEmpty(eshopProductsDbConnectionString, nameof(eshopProductsDbConnectionString));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddWolverineHttp();

builder.Services.AddProducts(eshopProductsDbConnectionString);

builder.Host.UseProjects(assemblies);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

app.MapWolverineEndpoints(opts =>
{
    opts.UseFluentValidationProblemDetailMiddleware();
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
    await dbContext.Database.MigrateAsync();
    
    if (app.Environment.IsDevelopment())
    {
        // TODO: separate this into separate runnable project
        await ProductSeeder.EnsureSeededAsync(dbContext);
    }
}

app.Run();
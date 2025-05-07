using Ardalis.GuardClauses;
using MikesEshop.Host;
using MikesEshop.Products;
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

app.Run();
using System.Reflection;
using Ardalis.GuardClauses;
using MikesEshop.Products;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddWolverineHttp();

var eshopProductsDbConnectionString = builder.Configuration.GetConnectionString("EshopProductsDb");
var assemblies = builder.Configuration.GetSection("WolverineEndpointAssemblies").Get<string[]>() ?? [];

Guard.Against.NullOrEmpty(eshopProductsDbConnectionString, nameof(eshopProductsDbConnectionString));

builder.Services.AddProducts(eshopProductsDbConnectionString);

builder.Host.UseWolverine(opts =>
{
    foreach (var assembly in assemblies) opts.Discovery.IncludeAssembly(Assembly.Load(assembly));
    
    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();
    
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWolverineEndpoints();

app.Run();
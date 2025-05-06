using Ardalis.GuardClauses;
using MikesEshop.Products;

var builder = WebApplication.CreateBuilder(args);

var eshopProductsDbConnectionString = builder.Configuration.GetConnectionString("EshopProductsDb");
Guard.Against.NullOrEmpty(eshopProductsDbConnectionString, nameof(eshopProductsDbConnectionString));

builder.Services.AddProducts(eshopProductsDbConnectionString);

var app = builder.Build();
app.Run();
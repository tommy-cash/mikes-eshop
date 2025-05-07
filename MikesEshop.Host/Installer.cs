using System.Reflection;
using Microsoft.OpenApi.Models;
using Wolverine;
using Wolverine.FluentValidation;

namespace MikesEshop.Host;

internal static class Installer
{
    internal static IHostBuilder UseProjects(this IHostBuilder host, string[] assemblies)
    {
        return host.UseWolverine(opts =>
        {
            foreach (var assembly in assemblies) opts.Discovery.IncludeAssembly(Assembly.Load(assembly));
    
            opts.Policies.AutoApplyTransactions();
            opts.Policies.UseDurableLocalQueues();
    
            opts.UseFluentValidation();
        });
    }
    
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });

            options.DocInclusionPredicate((docName, apiDesc) =>
                apiDesc.RelativePath?.StartsWith(docName) ?? false);
        });
    }
}
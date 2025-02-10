using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;

namespace Ordering;

public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<OrderingDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        
        return services;
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        app.UseMigration<OrderingDbContext>();
        
        return app;
    }
}
using Microsoft.EntityFrameworkCore;
using Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;
using Poc_WebPortalHiP.Api.Infra.Contexts;
using Poc_WebPortalHiP.Api.Infra.Repositories;

namespace Poc_WebPortalHiP.Api.Infra;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        
        services.AddScoped<BaseApplicationDbContext>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<ApplicationDbContext>();
        });
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
    
    public static void UseMigrations(this IApplicationBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
}
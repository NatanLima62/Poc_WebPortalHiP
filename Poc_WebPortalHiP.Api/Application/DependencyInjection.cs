using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Poc_WebPortalHiP.Api.Application.Contracts;
using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Poc_WebPortalHiP.Api.Application.Services;
using Poc_WebPortalHiP.Api.Core.Settings;
using Poc_WebPortalHiP.Api.Domain.Entities;
using Poc_WebPortalHiP.Api.Infra;
using ScottBrady91.AspNetCore.Identity;

namespace Poc_WebPortalHiP.Api.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<AzureAdSettings>(configuration.GetSection("AzureAdSettings"));
    }
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddRepositories();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        AplicarServices(services);
    }

    private static void AplicarServices(this IServiceCollection services)
    {
        services
            .AddScoped<INotificator, Notificator>()
            .AddScoped<IPasswordHasher<Usuario>, Argon2PasswordHasher<Usuario>>();

        services
            .AddScoped<IAuthService, AuthService>() 
            .AddScoped<IUsuarioService, UsuarioService>();
    }
}
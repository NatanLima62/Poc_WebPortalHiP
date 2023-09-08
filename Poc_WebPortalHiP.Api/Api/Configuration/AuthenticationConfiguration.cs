using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Poc_WebPortalHiP.Api.Core.Settings;

namespace Poc_WebPortalHiP.Api.Api.Configuration;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(appSettingsSection);
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                configuration.Bind("AzureAd", options);
                options.TokenValidationParameters.NameClaimType = "name";
            }, options => { configuration.Bind("AzureAd", options); });
        
        services
            .AddJwksManager()
            .UseJwtValidation();
        
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
    } 
}
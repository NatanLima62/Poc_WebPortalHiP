using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Poc_WebPortalHiP.Api.Core.Settings;

namespace Poc_WebPortalHiP.Api.Api.Configuration;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(appSettingsSection);
        
        var appSettings = appSettingsSection.Get<JwtSettings>();
        // services
        //     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.RequireHttpsMetadata = true;
        //         options.SaveToken = true;
        //         options.IncludeErrorDetails = true; // <- great for debugging
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidateLifetime = true,
        //             ValidateIssuerSigningKey = true,
        //             ValidIssuer = appSettings.Emissor,
        //             ValidAudiences = appSettings.Audiences()
        //         };
        //     });
        
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
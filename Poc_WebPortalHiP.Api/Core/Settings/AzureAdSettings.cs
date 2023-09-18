namespace Poc_WebPortalHiP.Api.Core.Settings;

public class AzureAdSettings
{
    public string TenantId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public string Scopes { get; set; } = string.Empty;
}
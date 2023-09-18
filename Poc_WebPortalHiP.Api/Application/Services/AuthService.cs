using AutoMapper;
using Microsoft.Extensions.Options;
using Poc_WebPortalHiP.Api.Application.Contracts;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Poc_WebPortalHiP.Api.Core.Settings;
using RestSharp;

namespace Poc_WebPortalHiP.Api.Application.Services;

public class AuthService : BaseServices, IAuthService
{
    private readonly AzureAdSettings _azureAdSettings;

    private const string url =
        $"https://login.live.com/oauth20_authorize.srf?scope=openid+profile+email+offline_access&response_type=code&client_id=51483342-085c-4d86-bf88-cf50c7252078&response_mode=form_post&redirect_uri=https%3a%2f%2flogin.microsoftonline.com%2fcommon%2ffederation%2foauth2msa&state=rQQIARAA02I20jOwUklNsUhOTrZM1jVJTk3TNUlJMtS1NDcy0000TjZONrBMMk2yNC4S4hJQK_DlmLL9mee2N8c2drwUMJzFyBmfk1mWqpecn7uKUTCjpKTASl8_Jz85MScjv7hEfwcj4wVGxheMjLeY-P0dS0syjEBEflFmVeoqZq3EgkygamLs3sSsYmiRamJsaWGum2aakqJrkpiSrJuYaGoO1GCQZJRqZGmWlGh5itmBeDP1A_KT48NTkwLyi0oSczwyA3SCUhNTbjAzXmBhfMXCY8BsxcHBJcAgwaDA8IOFcREr0PuyntX3lHrMXacfKA9-vXMSwylW_SCDnMw8H2_L4DynckPHLGfPZONyS0-fSN-iImPtRJeKXCcnM8sKQ2Mfx0BbcyvDCWyMH9gYO9gZdnEKoIfWBh7GA7wMP_hWnjrxcf7ft289XvHrZJZYVKQHuZqmRqRUGeX4uxuHukQGpZZlhpUV6bsZGIVX6Wd7hJdb-OamBtoCAA2&estsfed=1&uaid=084d7026b79449e6b6ecc6b188e91031&fci=ed8ccc9c-4cef-4db1-9726-a3c3c09b5b93";

    public AuthService(IMapper mapper, INotificator notificator, IOptions<AzureAdSettings> azureAdSettings) : base(
        mapper, notificator)
    {
        _azureAdSettings = azureAdSettings.Value;
    }

    public async Task<string> GerarLink()
    {
        // Passo 1: Redirecionar o Usuário para a Página de Login do Azure AD
        var authorizationCode = "";

        if (!string.IsNullOrEmpty(authorizationCode))
        {
            // Passo 3: Trocar o Código de Autorização por um Token de Acesso
            var accessToken = ExchangeAuthorizationCodeForAccessToken(authorizationCode);

            if (!string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("Token de acesso obtido com sucesso.");
                // Agora você pode usar o accessToken para autenticar solicitações à API do Azure AD ou outros recursos protegidos.
            }
            else
            {
                Console.WriteLine("Falha ao obter o token de acesso.");
            }
        }
        else
        {
            Console.WriteLine("Falha ao obter o código de autorização.");
        }

        Notificator.Handle("Falha ao chamar a requisição");
        return null;
    }

    private string ExchangeAuthorizationCodeForAccessToken(string authorizationCode)
    {
        var tokenUrl = $"https://{_azureAdSettings.Instance}/{_azureAdSettings.TenantId}/oauth2/token";

        var tokenRequest = new RestRequest();

        tokenRequest.AddParameter("client_id", _azureAdSettings.ClientId);
        tokenRequest.AddParameter("client_secret", _azureAdSettings.ClientSecret);
        tokenRequest.AddParameter("grant_type", "authorization_code");
        tokenRequest.AddParameter("code", authorizationCode);
        tokenRequest.AddParameter("redirect_uri", _azureAdSettings.RedirectUri);

        var client = new RestClient(tokenUrl);
        var response = client.Execute(tokenRequest);

        if (response.IsSuccessful)
        {
            var tokenResponse = response.Content;
            return tokenResponse;
        }
        else
        {
            Console.WriteLine("A solicitação para trocar o código de autorização por um token de acesso falhou.");
            Console.WriteLine("Código de status: " + response.StatusCode);
            Console.WriteLine("Resposta: " + response.Content);
            return null;
        }
    }
}
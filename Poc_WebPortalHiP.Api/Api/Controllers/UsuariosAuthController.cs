using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Poc_WebPortalHiP.Api.Core.Settings;
using RestSharp;
using Swashbuckle.AspNetCore.Annotations;

namespace Poc_WebPortalHiP.Api.Api.Controllers;

[AllowAnonymous]
[Route("auth/[controller]")]
public class UsuariosAuthController : BaseController
{
    private readonly AzureAdSettings _azureAdSettings;

    public UsuariosAuthController(INotificator notificator, IOptions<AzureAdSettings> azureAdSettings) : base(notificator)
    {
        _azureAdSettings = azureAdSettings.Value;
    }

    //Gerar o link para a pagina de login
    [HttpGet]
    [SwaggerOperation(Summary = "Gerar link", Tags = new[] { "Auth - Usuário" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GerarLinkParaLogin()
    {
        var authorizationUrl =
            $"{_azureAdSettings.Instance}/{_azureAdSettings.TenantId}/oauth2/v2.0/authorize?response_type=code&client_id={_azureAdSettings.ClientId}&scope={_azureAdSettings.Scopes}&redirect_uri={_azureAdSettings.RedirectUri}";

        var client = new RestClient(authorizationUrl);
        var authorizationRequest = new RestRequest();


        var response = client.BuildUri(authorizationRequest);

        return OkResponse(response);
    }
    
    //Gerar o token
    [HttpPost]
    [SwaggerOperation(Summary = "Gerar Token", Tags = new[] { "Auth - Usuário" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GerarToken(string authorizationCode)
    {
        var tokenUrl = $"{_azureAdSettings.Instance}/{_azureAdSettings.TenantId}/oauth2/token";

        var client = new RestClient(tokenUrl);
        var tokenRequest = new RestRequest("", Method.Post);

        tokenRequest.AddParameter("client_id", _azureAdSettings.ClientId);
        tokenRequest.AddParameter("client_secret", _azureAdSettings.ClientSecret);
        tokenRequest.AddParameter("grant_type", "authorization_code");
        tokenRequest.AddParameter("code", authorizationCode);
        tokenRequest.AddParameter("redirect_uri", _azureAdSettings.RedirectUri);

        // Execute a solicitação
        var response = client.Execute(tokenRequest);

        if (response.IsSuccessful)
        {
            // Analise a resposta para obter o token de acesso e retorne-o
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
            if (tokenResponse != null)
            {
                return Ok(tokenResponse.AccessToken);
            }
            else
            {
                Console.WriteLine("Falha ao analisar a resposta do token.");
                return BadRequest("Falha ao analisar a resposta do token.");
            }
        }
        else
        {
            Console.WriteLine("A solicitação para trocar o código de autorização por um token de acesso falhou.");
            Console.WriteLine("Código de status: " + response.StatusCode);
            Console.WriteLine("Resposta: " + response.Content);
            return BadRequest("A solicitação para trocar o código de autorização por um token de acesso falhou.");
        }
    }

    private class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

}
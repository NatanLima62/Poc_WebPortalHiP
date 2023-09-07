using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc_WebPortalHiP.Api.Application.DTOs.Auth;
using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Poc_WebPortalHiP.Api.Api.Controllers;

[AllowAnonymous]
[Route("auth/[controller]")]
public class UsuariosAuthController : BaseController
{
    private readonly IAuthService _authService;

    public UsuariosAuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Obter todos", Tags = new[] { "Auth - Usuário" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody]UsuarioLoginDto dto)
    {
        var usuario = await _authService.Login(dto);
        return usuario != null ? OkResponse(usuario) : Unauthorized(new[] { "Usuário e/ou senha incorretos" });
    }
}
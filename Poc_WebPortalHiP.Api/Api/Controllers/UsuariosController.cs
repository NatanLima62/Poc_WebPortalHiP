using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc_WebPortalHiP.Api.Application.Contracts;
using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;
using Poc_WebPortalHiP.Api.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Poc_WebPortalHiP.Api.Api.Controllers;

[Route("/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(INotificator notificator, IUsuarioService usuarioService) : base(notificator)
    {
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation(Summary = "Cadastro de um Usuário", Tags = new[] { "CRUD - Usuário" })]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Adicionar([FromForm] AdicionarUsuarioDto dto)
    {
        return OkResponse(await _usuarioService.Adicionar(dto));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um Usuário", Tags = new[] { "CRUD - Usuário" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _usuarioService.ObterPorId(id));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos", Tags = new[] { "CRUD - Usuário" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterTodos()
    {
        return OkResponse(await _usuarioService.ObterTodos());
    }
}
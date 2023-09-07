using Poc_WebPortalHiP.Api.Application.DTOs.Auth;

namespace Poc_WebPortalHiP.Api.Application.DTOs.Usuario;

public interface IAuthService
{
    Task<TokenDto?> Login(UsuarioLoginDto usuarioLoginDto);
}
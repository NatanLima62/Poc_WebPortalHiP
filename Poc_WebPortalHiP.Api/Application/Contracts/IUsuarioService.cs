using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;

namespace Poc_WebPortalHiP.Api.Application.Contracts;

public interface IUsuarioService
{
    Task<UsuarioDto?> Adicionar(AdicionarUsuarioDto usuarioDto);
    Task<UsuarioDto?> ObterPorId(int id);
    Task<List<UsuarioDto>?> ObterTodos();
}
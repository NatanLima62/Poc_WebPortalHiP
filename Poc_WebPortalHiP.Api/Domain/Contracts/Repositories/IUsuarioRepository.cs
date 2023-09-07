using Poc_WebPortalHiP.Api.Domain.Entities;

namespace Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> ObterPorId(int id);
    Task<Usuario?> ObterPorEmail(string email);
    Task<List<Usuario>?> ObterTodos();
    void Cadastrar(Usuario usuario);
}
using Microsoft.EntityFrameworkCore;
using Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;
using Poc_WebPortalHiP.Api.Domain.Entities;
using Poc_WebPortalHiP.Api.Infra.Contexts;

namespace Poc_WebPortalHiP.Api.Infra.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(BaseApplicationDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return await Context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return await Context.Usuarios.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<List<Usuario>?> ObterTodos()
    {
        return await Context.Usuarios.ToListAsync();
    }

    public void Cadastrar(Usuario usuario)
    {
        Context.Usuarios.Add(usuario);
    }
}
using Poc_WebPortalHiP.Api.Domain.Contracts;

namespace Poc_WebPortalHiP.Api.Domain.Entities;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}
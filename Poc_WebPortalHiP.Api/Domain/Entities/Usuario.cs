using FluentValidation.Results;
using Poc_WebPortalHiP.Api.Domain.Contracts;
using Poc_WebPortalHiP.Api.Domain.Validators;

namespace Poc_WebPortalHiP.Api.Domain.Entities;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new UsuarioValidator().Validate(this);
        return validationResult.IsValid;
    }
}
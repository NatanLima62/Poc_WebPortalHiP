using FluentValidation;
using Poc_WebPortalHiP.Api.Domain.Entities;

namespace Poc_WebPortalHiP.Api.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("Nome não pode ser vazio")
            .Length(3, 150)
            .WithMessage("Nome deve ter no mínimo 3 e no máximo 150 caracteres");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage("Senha não pode ser vazio")
            .Length(8, 20)
            .WithMessage("Senha deve ter no mínimo 8 e no máximo 20 caracteres");

        RuleFor(u => u.Email)
            .EmailAddress();
    }
}
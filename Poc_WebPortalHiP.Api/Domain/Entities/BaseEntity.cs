using FluentValidation.Results;
using Poc_WebPortalHiP.Api.Domain.Contracts;

namespace Poc_WebPortalHiP.Api.Domain.Entities;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }

    public virtual bool Validar(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}
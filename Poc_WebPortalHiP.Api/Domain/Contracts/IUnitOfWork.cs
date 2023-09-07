namespace Poc_WebPortalHiP.Api.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
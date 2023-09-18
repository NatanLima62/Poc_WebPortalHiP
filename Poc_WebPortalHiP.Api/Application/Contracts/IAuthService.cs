namespace Poc_WebPortalHiP.Api.Application.Contracts;

public interface IAuthService
{
    Task<string> GerarLink();
}
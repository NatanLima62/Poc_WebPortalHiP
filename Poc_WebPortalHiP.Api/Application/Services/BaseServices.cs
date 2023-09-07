using AutoMapper;
using Poc_WebPortalHiP.Api.Application.Notifications;

namespace Poc_WebPortalHiP.Api.Application.Services;

public abstract class BaseServices
{
    protected readonly IMapper Mapper;
    protected readonly INotificator Notificator;

    protected BaseServices(IMapper mapper, INotificator notificator)
    {
        Mapper = mapper;
        Notificator = notificator;
    }
}
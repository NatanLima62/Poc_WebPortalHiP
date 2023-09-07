﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc_WebPortalHiP.Api.Api.Responses;
using Poc_WebPortalHiP.Api.Application.Notifications;

namespace Poc_WebPortalHiP.Api.Api.Controllers;

[Authorize]
public abstract class BaseController : Controller
{
    private readonly INotificator _notificator;

    protected BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }

    protected IActionResult NoContentResponse() => CustomResponse(NoContent());

    protected IActionResult CreatedResponse(string uri = "", object? result = null) =>
        CustomResponse(Created(uri, result));

    protected IActionResult OkResponse(object? result = null) => CustomResponse(Ok(result));

    protected IActionResult CustomResponse(IActionResult objectResult)
    {
        if (OperacaoValida)
        {
            return objectResult;
        }

        if (_notificator.IsNotFoundResourse)
        {
            return NotFound();
        }

        var response = new BadRequestResponse(_notificator.GetNotifications().ToList());
        return BadRequest(response);
    }

    private bool OperacaoValida => !(_notificator.HasNotification || _notificator.IsNotFoundResourse);
}
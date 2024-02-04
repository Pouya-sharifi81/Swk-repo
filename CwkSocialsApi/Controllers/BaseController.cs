using AutoMapper;
using CwkApplication.Enums;
using CwkApplication.Models;
using CwkSocialsApi;
using CwkSocialsApi.Contract.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocialsApi.Controllers;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoutes)]
[ApiController]
public class BaseController : ControllerBase
{

    protected IActionResult HandleErrorResponse(List<Error> errors)
    {
        var apiError = new ErrorResponse();

        if (errors.Any(e => e.code == ErrorCode.NotFound))
        {
            var error = errors.FirstOrDefault(e => e.code == ErrorCode.NotFound);

            apiError.StatusCode = 404;
            apiError.Statusphrase = "Not Found";
            apiError.TimeStamp = DateTime.Now;
            apiError.Errors.Add(error.Massage);

            return NotFound(apiError);
        }

        apiError.StatusCode = 400;
        apiError.Statusphrase = "Bad request";
        apiError.TimeStamp = DateTime.Now;
        errors.ForEach(e => apiError.Errors.Add(e.Massage));
        return StatusCode(400, apiError);
    }
}
using AutoMapper;
using CwkApplication.Enums;
using CwkApplication.Identities.Commaand;
using CwkApplication.Models;
using CwkSocialsApi.Contract.Common;
using CwkSocialsApi.Contract.Identity;
using CwkSocialsApi.Filters;
using CwkSocialsApi.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CwkSocialsApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoutes)]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;
        public IdentityController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediatR = mediator;
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        public async Task<IActionResult> Register(UserRegister registration, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterIdentity>(registration);
            var result = await _mediatR.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IdentityUserProfile>(result.PayLoad));
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login(Login login, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(login);
            var result = await _mediatR.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IdentityUserProfile>(result.PayLoad));
        }
        //public IActionResult HandleErrorResponse(List<Error> errors)
        //{
        //    var apiError = new ErrorResponse();

        //    if (errors.Any(e => e.code == ErrorCode.NotFound))
        //    {
        //        var error = errors.FirstOrDefault(e => e.code == ErrorCode.NotFound);

        //        apiError.StatusCode = 404;
        //        apiError.Statusphrase = "Not Found";
        //        apiError.TimeStamp = DateTime.Now;
        //        apiError.Errors.Add(error.Massage);

        //        return NotFound(apiError);
        //    }

        //    apiError.StatusCode = 400;
        //    apiError.Statusphrase = "Bad request";
        //    apiError.TimeStamp = DateTime.Now;
        //    errors.ForEach(e => apiError.Errors.Add(e.Massage));
        //    return StatusCode(400, apiError);
        //}
    }
}

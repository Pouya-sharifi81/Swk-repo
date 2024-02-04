using AutoMapper;
using CwkApplication.Enums;
using CwkApplication.UserProfiles.Commands;
using CwkApplication.UserProfiles.Queries;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkSocialsApi.Contract.Common;
using CwkSocialsApi.Contract.UserProfile.Request;
using CwkSocialsApi.Contract.UserProfile.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CwkSocialsApi.Controllers.v1;
using CwkApplication.Models;
using CwkSocialsApi.Filters;
namespace CwkSocialsApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUserProfile();
            var response = await _mediator.Send(query);
            var porofile = _mapper.Map<List<UserProfileResponse>>(response.PayLoad);
            return Ok(porofile);
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateUpdate profille)
        {
            var command = _mapper.Map<CreateUserCommand>(profille);
            var response = await _mediator.Send(command);
            var userProfile = _mapper.Map<UserProfileResponse>(response.PayLoad);

            return CreatedAtAction(nameof(GetUserProfileById) , new {id = userProfile.UserProfileId} , userProfile);
        }
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpGet]
        [ValidateModelAtribute]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var Query = new GetUserProfileById { UserProfileId =Guid.Parse(id) };
            var respons = await _mediator.Send(Query);

            
            if(respons is null)
            {
               return HandleErrorResponse(respons.Errors); return NotFound();
            }

            var userProfile = _mapper.Map<UserProfileResponse>(respons.PayLoad);
            return Ok(userProfile);
           
        }
        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> UpdateUserProfile(string id , UserProfileResponse updateProfile)
        {
            var command =  _mapper.Map<UpdateUserProfileBasicInfo>(updateProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
           
        }

       
    

        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfile() { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();

        }

        private IActionResult HandleErrorResponse(List<Error> errors)
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


}

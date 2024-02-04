
using AutoMapper;
using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Commands;
using CwkApplication.Post.Queries;
using CwkApplication.UserProfiles.Commands;
using CwkApplication.UserProfiles.Queries;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkSocialsApi.Contract.Common;
using CwkSocialsApi.Contract.Post.Request;
using CwkSocialsApi.Contract.Post.Responses;
using CwkSocialsApi.Contract.UserProfile.Responses;
using CwkSocialsApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CwkSocialsApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoutes)]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PostController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var post = new GetAllPost();
            var result = await _mediator.Send(post);
            var mapper = _mapper.Map<List<PostResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapper);
        }

        [HttpGet]
        [Route(ApiRoutes.Post.GetById)]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = new GetPostById() { PostId = Guid.Parse(id) };
            var result = await _mediator.Send(post);
            var mappeer = _mapper.Map<PostResponse>(result.PayLoad);
            return Ok(mappeer);

        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate newpost)
        {
            var command = new CreatePost()
            {
                UserProfileId =Guid.Parse( newpost.UserProfileId),
                TextContent = newpost.TextContent
            };
            var response = await _mediator.Send(command);

            var postnew = _mapper.Map<PostResponse>(response.PayLoad);
            return CreatedAtAction(nameof(GetPostById), new { id = postnew.PostId }, postnew);
        }
        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModelAtribute]
        public async Task<IActionResult> UpdatePostText(string id, PostUpdate postUpdate)
        {
            var command = new UpdatePostText()
            {
                newText = postUpdate.Text,
                postId = Guid.Parse(id)
            };
            var result = await _mediator.Send(command);
            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();

        }
        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var command = new DeletePost()
            {

                postId = Guid.Parse(id)
            };
            var result = await _mediator.Send(command);
            return result.IsError ? HandleErrorResponse(result.Errors) : NotFound();

        }
        [HttpGet]
        [Route(ApiRoutes.Post.PostComments)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetCommentsByPostId(string postId)
        {
            var Query = new GetPostComments() { PostId = Guid.Parse(postId) };
            var result = await _mediator.Send(Query);
            if (result.IsError) HandleErrorResponse(result.Errors);
            var comments = _mapper.Map<List<PostCommentResponse>>(result.PayLoad);
            return Ok(comments);
          
        }
        [HttpPost]
        [Route(ApiRoutes.Post.PostComments)]
       
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] PostCommentCreate comment)
        {
            var isValidGuid = Guid.TryParse(comment.userProfileId, out Guid userProfileId);

            var command = new AddPostComment()
            {
                PostId = Guid.Parse(postId),
                UserProfileId = userProfileId,
                CommentText = comment.Text
            };

            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newComment = _mapper.Map<PostCommentResponse>(result.PayLoad);

            return Ok(newComment);


        }


        //this method for hanlde error
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

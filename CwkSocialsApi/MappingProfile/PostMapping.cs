using AutoMapper;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkSocialsApi.Contract.Post.Responses;

namespace CwkSocialsApi.MappingProfile
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<PostModel, PostResponse>();
            CreateMap<PostComment, PostCommentResponse>();
        }
    }
}

using AutoMapper;
using CwkApplication.UserProfiles.Commands;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using CwkSocialsApi.Contract.UserProfile.Request;
using CwkSocialsApi.Contract.UserProfile.Responses;

namespace CwkSocialsApi.MappingProfile
{
    public class UserProfileMapping :Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfileCreateUpdate, CreateUserCommand>();
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}

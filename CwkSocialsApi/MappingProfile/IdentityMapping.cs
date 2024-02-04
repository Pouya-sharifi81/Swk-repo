using AutoMapper;
using CwkApplication.Identities.Commaand;
using CwkApplication.Identities.Dtos;
using CwkSocialsApi.Contract.Identity;
using CwkSocialsApi.Register;

namespace CwkSocialsApi.MappingProfile
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<UserRegister, RegisterIdentity>();
            CreateMap<Login, LoginCommand>();
            CreateMap<IdentityUserProfileDto, IdentityUserProfile>();

        }
    }
}


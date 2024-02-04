using AutoMapper;
using CwkApplication.Identities.Commaand;
using CwkApplication.UserProfiles.Commands;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CwkApplication.MappingProfile
{
    internal class UserProfileMap :Profile
    {
        public UserProfileMap()
        {
            CreateMap<CreateUserCommand, BasicInfo>();
          
        }
    }
}

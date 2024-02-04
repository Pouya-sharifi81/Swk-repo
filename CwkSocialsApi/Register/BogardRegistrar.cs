
using CwkApplication.Identities.Commaand;
using CwkApplication.Identities.CommandHandler;
using CwkApplication.Models;
using CwkApplication.UserProfiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CwkSocialsApi.Register
{
    public class BogardRegistrar : IWebApplicationBuilder
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfile));
            builder.Services.AddMediatR(typeof(GetAllUserProfile));
           

        }
    }
}

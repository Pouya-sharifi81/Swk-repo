using CwkApplication.services;

namespace CwkSocialsApi.Register
{

    public class ApplicationLayerRegistrar : IWebApplicationBuilder
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
        }
    }
}

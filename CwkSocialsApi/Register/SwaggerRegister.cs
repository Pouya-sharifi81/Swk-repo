
using CwkSocialsApi.options;

namespace CwkSocialsApi.Register
{
    public class SwaggerRegister : IWebApplicationBuilder
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        }
    }
}

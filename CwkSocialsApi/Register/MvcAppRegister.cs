
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CwkSocialsApi.Register
{
    public class MvcAppRegister : IWebApplicartionRegister
    {
        public void RegisterPipeLineComponent(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.ApiVersion.ToString());
                }
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthorization();
            app.MapControllers();

        }
    }
}

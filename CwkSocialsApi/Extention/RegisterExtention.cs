using CwkSocialsApi.Register;

namespace CwkSocialsApi.Extention
{
    public static class RegistrarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilder>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        public static void RegisterPipeLineComponent(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicartionRegister>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterPipeLineComponent(app);
            }
        }

        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegister
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
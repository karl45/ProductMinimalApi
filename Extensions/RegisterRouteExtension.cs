using LoginProductMinimalApi.RouteConfiguration;

namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterRouteExtension
    {
        public static void RegisterEndPointConfiguration(this WebApplication webApplication)
        {
            var endPointConfigurations = typeof(Program).Assembly
                .GetTypes()
                .Where(e => e.IsAssignableTo(typeof(IRouteConfiguration)) && !e.IsAbstract && !e.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IRouteConfiguration>();

            foreach (var endPointConfiguration in endPointConfigurations)
            {
                endPointConfiguration.ConfigureEndPoints(webApplication);
            }

        }
    }
}

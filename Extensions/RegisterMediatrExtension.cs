using LoginProductMinimalApi.Handlers;
using MediatR;

namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterMediatrExtension
    {

        public static void AddMediatr(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
            webApplicationBuilder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingCQRSPipelineBehaviour<,>));
        }
    }
}

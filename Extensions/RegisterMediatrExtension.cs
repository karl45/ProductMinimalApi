namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterMediatrExtension
    {

        public static void AddMediatr(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
        }
    }
}

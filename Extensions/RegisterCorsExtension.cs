namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterCorsExtension
    {
        public static void RegisterCors(this WebApplicationBuilder webApplicationBuilder)
        {
            var client_url = webApplicationBuilder.Configuration["FrontEnd:APIURL"];
            webApplicationBuilder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(client_url!)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
    }
}

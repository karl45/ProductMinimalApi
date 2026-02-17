namespace LoginProductMinimalApi.Extensions
{
    public static class RegisterAntiforgeryTokenExtension
    {
        public static void RegisterAntiforgeryToken(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "LP-CSRF-TOKEN";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.HeaderName = "LP-CSRF-TOKEN";
            });
        }
    }
}

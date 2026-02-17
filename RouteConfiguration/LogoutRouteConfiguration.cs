using Microsoft.AspNetCore.Antiforgery;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class LogoutEndPoint_Configuration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var logout = webApplication.MapGroup("/logout");
            logout.MapPost("/", Logout);
        }

        public async Task<IResult> Logout(HttpResponse response, IAntiforgery antiforgery, HttpContext httpContext)
        {
            await antiforgery.ValidateRequestAsync(httpContext);
            response.Cookies.Delete("access_token", new CookieOptions()
            {

                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)

            });
            return Results.Ok(new { message = "Logged out successfully" });
        }
    }
}

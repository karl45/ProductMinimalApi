using LoginProductMinimalApi.Models;
using MediatR;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class LoginRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var login = webApplication.MapGroup("/login");
            login.MapPost("/", Login);
        }

        public async Task<IResult> Login(IMediator mediator, LoginRequest model, HttpContext httpContext)
        {
            var response = await mediator.Send(model);
            if (response == null)
            {
                return Results.StatusCode(808);
            }
            else
            {
                httpContext.Response.Cookies.Append("access_token", response.Token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                });

                httpContext.Response.Cookies.Append("refresh_token", response.RefreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

            }
            return Results.Ok(response);
        }
    }
}

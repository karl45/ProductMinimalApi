using LoginProductMinimalApi.RequestModels;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class RefreshTokenRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var refresh = webApplication.MapGroup("/refresh");
            refresh.MapGet("/", Refresh);
        }

        public async Task<IResult> Refresh(HttpRequest request, IMediator mediator, HttpContext httpContext)
        {
            if (!request.Cookies.TryGetValue("refresh_token", out var refreshToken))
            {
                return Results.Unauthorized();
            }

            var response = await mediator.Send(new RefreshTokenRequest { RefreshToken = refreshToken });

            httpContext.Response.Cookies.Append("access_token", response.Token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddSeconds(10)
            });

            httpContext.Response.Cookies.Append("refresh_token", response.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });



            return Results.Ok(response);
        }
    }
}

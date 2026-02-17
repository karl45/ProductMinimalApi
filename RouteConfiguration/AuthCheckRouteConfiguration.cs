using System.Security.Claims;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class AuthCheckRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var check = webApplication.MapGroup("/check");
            check.MapGet("/", CheckAuth)
                .RequireAuthorization();
        }
        public async Task<IResult> CheckAuth(ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity?.IsAuthenticated ?? true)
            {
                return Results.Unauthorized();
            }
            else
            {
                return Results.Ok();
            }

        }
    }
}

using Microsoft.AspNetCore.Antiforgery;

namespace LoginProductMinimalApi.RouteConfiguration
{
    public class CsrfRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var csrf_token = webApplication.MapGroup("/csrf-token");
            csrf_token.MapGet("/", GetCsrfToken)
                .RequireAuthorization();
        }

        public IResult GetCsrfToken(IAntiforgery antiforgery, HttpResponse response, HttpContext httpContext)
        {
            var tokens = antiforgery.GetAndStoreTokens(httpContext);
            var csrfToken = tokens.RequestToken ?? throw new InvalidOperationException("CSRF Token is empty");
            return Results.Ok(new
            {
                csrfToken,
            });
        }
    }
}

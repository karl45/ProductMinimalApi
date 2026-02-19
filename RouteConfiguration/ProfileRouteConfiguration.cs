namespace LoginProductMinimalApi.RouteConfiguration
{
    public class ProfileRouteConfiguration : IRouteConfiguration
    {
        public void ConfigureEndPoints(WebApplication webApplication)
        {
            var profileGroup = webApplication.MapGroup("/profile");

            //profileGroup.MapGet("/", () => GetUserProfile)
            //    .RequireAuthorization();
        }

        //public async Task<IResult> GetUserProfile()
        //{

        //}
    }
}

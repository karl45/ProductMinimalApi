namespace LoginProductMinimalApi.ResponseModels
{
    public class LoginResponse
    {
        public required string Token { set; get; }

        public required string CsrfToken { set; get; }  

        public required string RefreshToken { set; get; }
    }
}

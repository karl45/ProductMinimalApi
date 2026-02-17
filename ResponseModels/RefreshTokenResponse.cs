namespace LoginProductMinimalApi.ResponseModels
{
    public class RefreshTokenResponse
    {
        public required string Token { set; get; }

        public string RefreshToken { set; get; }
    }
}

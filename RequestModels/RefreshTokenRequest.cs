using LoginProductMinimalApi.ResponseModels;
using MediatR;

namespace LoginProductMinimalApi.RequestModels
{
    public class RefreshTokenRequest: IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { set; get; }
    }
}

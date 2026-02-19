using LoginProductMinimalApi.ResponseModels;
using MediatR;

namespace LoginProductMinimalApi.Models
{
    public class LoginRequest: IRequest<LoginResponse>
    {

        public required string UserName { set; get; }

        public required string Password { set; get; }

    }
}

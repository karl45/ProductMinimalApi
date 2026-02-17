using LoginProductMinimalApi.ResponseModels;
using MediatR;

namespace LoginProductMinimalApi.Models
{
    public class LoginRequestModel: IRequest<LoginResponseModel>
    {

        public required string UserName { set; get; }

        public required string Password { set; get; }

    }
}

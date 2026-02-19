using LoginProductMinimalApi.ResponseModels;
using MediatR;

namespace LoginProductMinimalApi.RequestModels
{
    public class GetProfileRequest: IRequest<GetProfileResponse>
    {
        public string UserName { set; get; }
    }
}

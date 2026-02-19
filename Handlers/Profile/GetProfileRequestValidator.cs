using LoginProductMinimalApi.RequestModels;
using LoginProductMinimalApi.ResponseModels;
using MediatR;

namespace LoginProductMinimalApi.Handlers.Profile
{
    public class GetProfileRequestValidator : LoggingCQRSPipelineBehaviour<GetProfileRequest, GetProfileResponse>
    {
        public GetProfileRequestValidator(ILogger<LoggingCQRSPipelineBehaviour<GetProfileRequest, GetProfileResponse>> logger) : base(logger)
        {

        }
    }
}

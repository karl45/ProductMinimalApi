using AutoMapper;
using LoginProductMinimalApi.Entities;
using LoginProductMinimalApi.Models.Profile;
using LoginProductMinimalApi.Repositories.UserRepository;
using LoginProductMinimalApi.RequestModels;
using LoginProductMinimalApi.ResponseModels;

namespace LoginProductMinimalApi.Handlers.Profile
{
    public class GetProfileRequestHandler : BaseRequestHandler<GetProfileRequest, GetProfileResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetProfileRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task<GetProfileResponse> HandleInternal(GetProfileRequest request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                Password = string.Empty
            };

            var dbUser = await _userRepository.GetUser(user, cancellationToken) ?? throw new Exception("User doesn't exists");

            return new GetProfileResponse
            {
                User = _mapper.Map<UserModel>(dbUser)
            };

        }
    }
}

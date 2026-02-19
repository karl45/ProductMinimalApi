using LoginProductMinimalApi.Entities;

namespace LoginProductMinimalApi.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetUser(User user, CancellationToken token);
        Task<User?> GetUserByRefreshToken(string refreshToken, CancellationToken token);
        Task<User> UpdateUser(User client, CancellationToken token);
    }
}

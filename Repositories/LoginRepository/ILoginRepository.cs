using LoginProductMinimalApi.Entities;

namespace LoginProductMinimalApi.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        Task<Client> Authorize(string username, string password, CancellationToken token);
        Task<Client> GetClientByRefreshToken(string refreshToken, CancellationToken token);
        Task<Client> UpdateClientRefreshToken(Client client, CancellationToken token);
    }
}

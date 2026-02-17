using LoginProductMinimalApi.DbClient;
using LoginProductMinimalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginProductMinimalApi.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository

    {
        private LoginProductDbContext _dbContext;

        public LoginRepository(LoginProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client?> GetClientByRefreshToken(string refreshToken, CancellationToken token)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.RefreshToken == refreshToken, token);
            return client;
        }

        public async Task<Client> UpdateClientRefreshToken(Client client, CancellationToken token) { 
            _dbContext.Update(client);
            await _dbContext.SaveChangesAsync(token);

            return client;
        }

        public async Task<Client> Authorize(string username, string password, CancellationToken token)
        {
            var user = await _dbContext.Clients.FirstOrDefaultAsync(token);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException();
            }

            return user;
        }
    }
}

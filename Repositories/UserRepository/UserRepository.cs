using LoginProductMinimalApi.DbClient;
using LoginProductMinimalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginProductMinimalApi.Repositories.UserRepository
{
    public class UserRepository : IUserRepository

    {
        private UserProductDbContext _dbContext;

        public UserRepository(UserProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User?> GetUserByRefreshToken(string refreshToken, CancellationToken token)
        {
            var client = await _dbContext.Users.FirstOrDefaultAsync(c => c.RefreshToken == refreshToken && c.BlockedTime == null, token);
            return client;
        }

        public async Task<User> UpdateUser(User client, CancellationToken token) { 
            _dbContext.Update(client);
            await _dbContext.SaveChangesAsync(token);
            return client;
        }

        public async Task<User> GetUser(User user, CancellationToken token)
        {
            var dbUser = await _dbContext.Users.Where(x => x.UserName == user.UserName && x.BlockedTime == null).FirstOrDefaultAsync(token);

            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                throw new UnauthorizedAccessException();
            }

            return dbUser;
        }
    }
}

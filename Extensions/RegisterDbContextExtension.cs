using LoginProductMinimalApi.DbClient;
using LoginProductMinimalApi.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace LoginProductMinimalApi.Extensions
{
    public static  class RegisterDbContextExtension
    {
        public static void RegisterDbContext(this WebApplicationBuilder webApplicationBuilder)
        {
            var connection_string = webApplicationBuilder.Configuration["PGSQL:ConnectionString"];

            webApplicationBuilder.Services.AddDbContext<UserProductDbContext>(opt =>
            {
                opt.UseNpgsql(connection_string);
            });

            webApplicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}

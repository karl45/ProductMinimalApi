using LoginProductMinimalApi.DbClient;
using LoginProductMinimalApi.Repositories.LoginRepository;
using Microsoft.EntityFrameworkCore;

namespace LoginProductMinimalApi.Extensions
{
    public static  class RegisterDbContextExtension
    {
        public static void RegisterDbContext(this WebApplicationBuilder webApplicationBuilder)
        {
            var connection_string = webApplicationBuilder.Configuration["PGSQL:ConnectionString"];

            webApplicationBuilder.Services.AddDbContext<LoginProductDbContext>(opt =>
            {
                opt.UseNpgsql(connection_string);
            });

            webApplicationBuilder.Services.AddScoped<ILoginRepository, LoginRepository>();

        }
    }
}



namespace LoginProductMinimalApi.Extensions.mapper
{
    public static class RegisterMapperConfiguration
    {
        public static void AddMapperConfiguration(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<MapperProfile>();
            },typeof(Program));
        }
    }
}

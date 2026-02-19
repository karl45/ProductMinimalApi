namespace LoginProductMinimalApi.Extensions
{
    public static class AddLoggingExtension
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
        }
    }
}

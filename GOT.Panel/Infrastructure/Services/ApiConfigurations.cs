namespace GOT.Panel.Infrastructure.Services
{
    public static class ApiConfigurations
    {
        public static string BaseUrl { get; set; }

        static ApiConfigurations()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            BaseUrl = builder.Build().GetSection("ApiSettings:BaseUrl").Value ?? string.Empty;


        }
    }
}

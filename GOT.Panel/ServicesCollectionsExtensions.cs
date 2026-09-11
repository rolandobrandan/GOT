using GOT.Panel.Infrastructure.Services;

namespace GOT.Panel
{
    public static class ServicesCollectionsExtensions
    {
        public static void AddServicesCollections(this IServiceCollection services)
        {
            services.AddScoped(h => new HttpClient { BaseAddress = new Uri(ApiConfigurations.BaseUrl) });
        }
    }
}

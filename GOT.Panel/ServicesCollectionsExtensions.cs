using GOT.Panel.Infrastructure.Api;
using GOT.Panel.Infrastructure.Services;

namespace GOT.Panel
{
    public static class ServicesCollectionsExtensions
    {
        public static void AddServicesCollections(this IServiceCollection services)
        {
            services.AddScoped(h => new HttpClient { BaseAddress = new Uri(ApiConfigurations.BaseUrl) });


            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<IKingdomService, KingdomService>();
            services.AddScoped<IDeathCategoryService, DeathCategoryService>();
            services.AddScoped<IBattleTypeService, BattleTypeService>();






            //Con AddScoped:
            // 1 - Clic en la página -> Se crea 1 objeto SeasonService en la RAM.
            // 2 - Llamada 1, Llamada 2 y Llamada 3 usan ese mismo objeto.
            // 3 - Carga la página -> Se elimina ese objeto de la RAM.

            //Con AddTransient:
            // 1 - Clic en la página.
            // 2 - Llamada 1 -> Se crea Objeto #1 en RAM y se borra.
            // 3 - Llamada 2 -> Se crea Objeto #2 en RAM y se borra.
            // 4 - Llamada 3 -> Se crea Objeto #3 en RAM y se borra.

            //Con Singleton

            //El Servidor se enciende / El primer usuario hace Clic:
            //Llamada 1 -> Se crea el Objeto #1 en RAM (porque es la primera vez que se pide).
            //
            //Durante el resto del día (Usuario 1 o cualquier otro usuario):
            //2. Llamada 2 -> Reutiliza el Objeto #1 que ya estaba en RAM.
            //3. Llamada 3 -> Reutiliza el Objeto #1 que sigue en RAM.
            //4. Llamada 100 -> Reutiliza el Objeto #1.
            //Fin del día:
            //5. Se apaga o se reinicia el servidor -> Recién ahí se borra el Objeto #1 de la RAM.
        }
    }
}

using System.Net;

namespace WAMiTramiteGestion.Handlers
{
    public static class HttpClientConfigurationExtensions
    {
        public static IServiceCollection AddApiHttpClients(this IServiceCollection services, string baseUrl)
        {
            services.AddScoped(sp =>
            {
                var handler = new HttpClientHandler()
                {
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                };

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseUrl)
                };

                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                return httpClient;
            });

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            });

            return services;
        }
    }
}

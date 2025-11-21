namespace WAMiTramiteGestion.Services.Configuration
{
    public interface IApiConfigurationService
    {
        string GetApiBaseUrl();
    }

    public class ApiConfigurationService : IApiConfigurationService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public ApiConfigurationService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string GetApiBaseUrl()
        {
            return _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:5001";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WAMiTramiteGestion.Services.VerificationService
{
    public class TokenVerificationService : ITokenVerificationService
    {
        private readonly HttpClient _httpClient;
        public TokenVerificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> TokenExistsOrIsValidAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("verify");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar el token: {ex.Message}");
                return false;
            }
        }
    }
}
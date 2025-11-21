using System.Net;
using System.Text.Json;

namespace WAMiTramiteGestion.Services.Base
{
    public abstract class BaseApiService
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        protected HttpClient GetConfiguredHttpClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            return client;
        }

        protected HttpClient GetConfiguredHttpClientWithAuth(string token)
        {
            var client = GetConfiguredHttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        // Método especializado para upload de archivos
        protected async Task<bool> PostFileAsync(string endpoint, Stream fileStream, string fileName)
        {
            try
            {
                var client = GetConfiguredHttpClient();
                using var content = new MultipartFormDataContent();
                using var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "file", fileName);

                var response = await client.PostAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en upload de archivo {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<T?> GetAsync<T>(string endpoint) where T : class
        {
            try
            {
                var client = GetConfiguredHttpClient();

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
                    return result;
                }

                await HandleErrorResponse(response);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GET {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<T?> PostAsync<T>(string endpoint, object data) where T : class
        {
            try
            {
                var client = GetConfiguredHttpClient();

                var response = await client.PostAsJsonAsync(endpoint, data, _jsonOptions);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
                    return result;
                }

                await HandleErrorResponse(response);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en POST {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<bool> PostAsync(string endpoint, object data)
        {
            try
            {
                var client = GetConfiguredHttpClient();

                var response = await client.PostAsJsonAsync(endpoint, data, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en POST {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<T?> PutAsync<T>(string endpoint, object data) where T : class
        {
            try
            {
                var client = GetConfiguredHttpClient();
                var response = await client.PutAsJsonAsync(endpoint, data, _jsonOptions);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
                }

                await HandleErrorResponse(response);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PUT {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<bool> PutAsync(string endpoint, object data)
        {
            try
            {
                var client = GetConfiguredHttpClient();
                var response = await client.PutAsJsonAsync(endpoint, data, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PUT {endpoint}: {ex.Message}");
                throw;
            }
        }

        protected async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var client = GetConfiguredHttpClient();
                var response = await client.DeleteAsync(endpoint);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DELETE {endpoint}: {ex.Message}");
                throw;
            }
        }

        private async Task HandleErrorResponse(HttpResponseMessage response)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error Response: {response.StatusCode} - {errorContent}");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException("No autorizado. Por favor, inicie sesión nuevamente.");
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException("Acceso denegado. No tiene permisos para realizar esta acción.");
                case HttpStatusCode.NotFound:
                    throw new Exception("Recurso no encontrado.");
                case HttpStatusCode.BadRequest:
                    throw new Exception($"Solicitud inválida: {errorContent}");
                case HttpStatusCode.InternalServerError:
                    throw new Exception("Error interno del servidor. Por favor, intente más tarde.");
                default:
                    throw new Exception($"Error en la petición: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }
}

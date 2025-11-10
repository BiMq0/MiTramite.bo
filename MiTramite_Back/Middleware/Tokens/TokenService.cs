using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace MiTramite_Back.Middleware.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public async Task ValidarToken(HttpContext context, Func<Task> next, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token no proporcionado");
                return;
            }

            try
            {
                // Validar que la configuración JWT esté disponible
                if (_jwtOptions.Value == null ||
                    string.IsNullOrEmpty(_jwtOptions.Value.Key) ||
                    string.IsNullOrEmpty(_jwtOptions.Value.Issuer) ||
                    string.IsNullOrEmpty(_jwtOptions.Value.Audience))
                {
                    Console.WriteLine("Error: Configuración JWT no válida");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Error de configuración del servidor");
                    return;
                }

                Console.WriteLine($"Validando token: {token.Substring(0, Math.Min(20, token.Length))}...");

                var handler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = _jwtOptions.Value.Issuer,
                    ValidAudience = _jwtOptions.Value.Audience,
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);

                Console.WriteLine("Token validado correctamente");
                context.User = principal;
                await next();
            }
            catch (SecurityTokenExpiredException)
            {
                Console.WriteLine("Token expirado");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token expirado");
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Token inválido: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al validar token: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Error de autenticación");
            }
        }
    }
}
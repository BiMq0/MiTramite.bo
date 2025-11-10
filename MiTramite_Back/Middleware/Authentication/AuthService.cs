using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiTramite_Shared.DTOs.FuncionarioDTOs;

namespace MiTramite_Back.Middleware.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        public AuthService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public Task<string> GenerarTokenFuncionario(FuncionarioAccesosDTO funcionarioDto)
        {
            var claims = new[]
            {
                new Claim("CodigoUsuario", funcionarioDto.CodigoFuncionario!),
                new Claim(ClaimTypes.Role, funcionarioDto.Rol!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddHours(2), // Token válido por 2 horas
                notBefore: DateTime.UtcNow // Token válido desde ahora
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(tokenString);
        }
    }
}
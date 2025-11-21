using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Shared.DTOs.RentistaDTOs;

namespace MiTramite_Back.Middleware.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerarTokenFuncionario(FuncionarioAccesosDTO funcionarioDto)
        {
            var claims = new List<Claim>
            {
                new Claim("CodigoUsuario", funcionarioDto.CodigoFuncionario!),
                new Claim(ClaimTypes.Role, funcionarioDto.Rol!)
            };

            return Task.FromResult(GenerarJWT(claims));
        }

        public Task<string> GenerarTokenRentista(RentistaCurrentDataDTO rentistaDto)
        {
            var claims = new List<Claim>
            {
                new Claim("IdRentista", rentistaDto.IdRentista!.ToString())
            };

            return Task.FromResult(GenerarJWT(claims));
        }

        private string GenerarJWT(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(2),
                notBefore: DateTime.UtcNow
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public CookieOptions ConfigurarCookie()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(2),
                Path = "/"
            };

        }
    }
}
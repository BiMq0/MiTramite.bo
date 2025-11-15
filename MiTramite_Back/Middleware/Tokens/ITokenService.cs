using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MiTramite_Shared.DTOs.FuncionarioDTOs;
using MiTramite_Shared.DTOs.RentistaDTOs;

namespace MiTramite_Back.Middleware.Tokens
{
    public interface ITokenService
    {
        Task ValidarToken(HttpContext context, Func<Task> next, string token);
        Task<string> GenerarTokenFuncionario(FuncionarioAccesosDTO funcionarioDto);
        Task<string> GenerarTokenRentista(RentistaCurrentDataDTO funcionarioDto);
        SigningCredentials ConfigurarCredencialesToken();
        JwtSecurityToken CrearToken(Claim[] claims, int expirationHours);
    }
}
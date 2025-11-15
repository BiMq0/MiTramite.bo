using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep;
using MiTramite_Back.Middleware.Tokens;
using MiTramite_Shared.DTOs.FuncionarioDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public FuncionarioService(IFuncionarioRepository repository, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor ?? throw new InvalidOperationException("No se pudo acceder al HttpContext."); ;
            _tokenService = tokenService;
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesionFuncionario(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default)
        {
            var funcionarioAccesosDTO = await _repository.IniciarSesionFuncionarioAsync(funcionarioLogin, cancellationToken);
            if (funcionarioAccesosDTO != null)
            {
                var token = await _tokenService.GenerarTokenFuncionario(funcionarioAccesosDTO);
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("token", token, ConfigurarCookie());
            }
            return funcionarioAccesosDTO ?? throw new Exception("Error al iniciar sesión del funcionario");
        }

        public CookieOptions ConfigurarCookie()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            };
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.FuncionarioRep;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.FuncionarioDTOs;


namespace MiTramite_Back.Logica_De_Negocio.Services.FuncionarioSvc
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;

        public FuncionarioService(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<FuncionarioAccesosDTO> IniciarSesionFuncionario(FuncionarioLoginDTO funcionarioLogin, CancellationToken cancellationToken = default)
        {
            var resultado = await _repository.IniciarSesionFuncionarioAsync(funcionarioLogin, cancellationToken);
            return resultado;
        }
    }
}

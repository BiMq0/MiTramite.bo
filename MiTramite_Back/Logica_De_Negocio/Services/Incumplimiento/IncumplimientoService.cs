using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep;
using MiTramite_Shared.DTOs.Incumplimiento;

namespace MiTramite_Back.Logica_De_Negocio.Services.Incumplimiento
{
    public class IncumplimientoService : IIncumplimientoService
    {
        private readonly IIncumplimientoRepository _repository;

        public IncumplimientoService(IIncumplimientoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IncumplimientoRegistroDTO>> ObtenerTodosAsync()
        {
            var incumplimientos = await _repository.ObtenerTodosAsync();
            return incumplimientos.Select(i => new IncumplimientoRegistroDTO
            {
                IdSolicitudTramite = i.IdSolicitudTramite,
                NombreRentista = $"{i.SolicitudTramite?.Rentista?.Nombres} {i.SolicitudTramite?.Rentista?.ApellidoPaterno}",
                NombreFuncionarioOriginal = $"{i.Funcionario?.Nombres} {i.Funcionario?.ApellidoPaterno}",
                NombreFuncionarioReasignado = $"{i.FuncionarioReasignado?.Nombres} {i.FuncionarioReasignado?.ApellidoPaterno}",
                FechaIncumplimiento = i.FechaReasignacion
            }).ToList();
        }

        public async Task<IncumplimientoDetalleDTO?> ObtenerPorIdTramiteAsync(long idSolicitudTramite)
        {
            var i = await _repository.ObtenerPorTramiteIdAsync(idSolicitudTramite);
            if (i == null) return null;

            return new IncumplimientoDetalleDTO
            {
                IdSolicitudTramite = i.IdSolicitudTramite,
                NombreRentista = $"{i.SolicitudTramite?.Rentista?.Nombres} {i.SolicitudTramite?.Rentista?.ApellidoPaterno} {i.SolicitudTramite?.Rentista?.ApellidoMaterno}",
                NombreFuncionarioOriginal = $"{i.Funcionario?.Nombres} {i.Funcionario?.ApellidoPaterno} {i.Funcionario?.ApellidoMaterno}",
                NombreFuncionarioReasignado = $"{i.FuncionarioReasignado?.Nombres} {i.FuncionarioReasignado?.ApellidoPaterno} {i.FuncionarioReasignado?.ApellidoMaterno}",
                FechaIncumplimiento = i.FechaReasignacion,
                NombreTipoTramite = i.SolicitudTramite?.TipoTramite?.Nombre ?? "Desconocido",
                CorreoRentista = i.SolicitudTramite?.Rentista?.Correo ?? "",
                CorreoFuncionarioOriginal = i.Funcionario?.Correo ?? "",
                CorreoFuncionarioReasignado = i.FuncionarioReasignado?.Correo ?? "",
                FechaSolicitudTramite = i.SolicitudTramite?.FechaSolicitud ?? DateTime.MinValue,
                FechaEstimadaEntrega = i.SolicitudTramite?.FechaEstimadaEntrega ?? DateTime.MinValue
            };
        }
    }
}

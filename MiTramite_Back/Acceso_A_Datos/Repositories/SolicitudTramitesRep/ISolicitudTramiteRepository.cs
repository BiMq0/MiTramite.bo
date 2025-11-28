using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramitesRep
{
    public interface ISolicitudTramiteRepository
    {
        Task<bool> CrearSolicitudTramiteAsync(SolicitudTramiteNuevoDTO solicitudNueva, CancellationToken cancellationToken = default);
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorRentistaAsync(long idRentista, CancellationToken cancellationToken = default);
        Task<SolicitudTramiteRegistroDTO?> ObtenerTramitePorIdAsync(long idSolicitudTramite, CancellationToken cancellationToken = default);
        Task<bool> CompletarTramiteAsync(long idSolicitudTramite, CancellationToken cancellationToken = default);
        Task<bool> RechazarTramiteAsync(long idSolicitudTramite, string motivo, CancellationToken cancellationToken = default);
        Task<(string CorreoRentista, string CorreoFuncionario, string NombreRentista, string NombreFuncionario, string NombreTramite)?> ObtenerCorreosTramiteAsync(long idSolicitudTramite, CancellationToken cancellationToken = default);
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario, CancellationToken cancellationToken = default);
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync(CancellationToken cancellationToken = default);
        Task<List<SolicitudTramite>> ObtenerTramitesParaBackgroundServiceAsync(CancellationToken cancellationToken = default);
        Task<Funcionario?> ObtenerFuncionarioConMayorDisponibilidadAsync(CancellationToken cancellationToken = default);
        Task<Funcionario?> ObtenerFuncionarioConMayorDisponibilidadParaReasignacionAsync(long idFuncionario, CancellationToken cancellationToken = default);
        Task<bool> ActualizarTramitePorIncumplimiento(SolicitudTramite tramite);
    }
}
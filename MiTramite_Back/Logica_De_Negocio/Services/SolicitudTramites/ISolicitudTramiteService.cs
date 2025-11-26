using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramitesSvc
{
    public interface ISolicitudTramiteService
    {
        Task<bool> CrearSolicitudTramiteAsync(SolicitudTramiteNuevoDTO solicitudNueva, CancellationToken cancellationToken = default);
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorRentistaAsync(long idRentista, CancellationToken cancellationToken = default);
        Task<SolicitudTramiteRegistroDTO?> ObtenerTramitePorIdAsync(long idSolicitudTramite, CancellationToken cancellationToken = default);
        Task<bool> CompletarTramiteAsync(long idSolicitudTramite, CancellationToken cancellationToken = default);
        Task<bool> RechazarTramiteAsync(long idSolicitudTramite, string motivo, CancellationToken cancellationToken = default);

        // Nuevos métodos
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario, CancellationToken cancellationToken = default);
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync(CancellationToken cancellationToken = default);
    }
}
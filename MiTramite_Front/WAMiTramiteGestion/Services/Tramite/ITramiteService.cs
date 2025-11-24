using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// #region DATOS DE EJEMPLO TEMPORALES - CAMBIAR A MiTramite_Shared.DTOs.TramiteDTOs CUANDO ESTÉ DISPONIBLE
using MiTramite_Front.Services.Tramite;
// #endregion

namespace WAMiTramiteGestion.Services
{
    public interface ITramiteService
    {
        #region Métodos de Funcionario

        /// <summary>
        /// Obtiene todos los trámites pendientes asignados al funcionario actual
        /// </summary>
        Task<List<SolicitudTramiteDTO>> ObtenerTramitesPendientes();

        /// <summary>
        /// Obtiene el historial de trámites completados del funcionario
        /// </summary>
        Task<List<SolicitudTramiteDTO>> ObtenerHistorialTramites();

        /// <summary>
        /// Obtiene los detalles completos de un trámite específico
        /// </summary>
        Task<DetallesTramiteDTO> ObtenerDetallesTramite(long idSolicitudTramite);

        /// <summary>
        /// Obtiene la lista de archivos subidos para un trámite
        /// </summary>
        Task<List<ArchivoTramiteDTO>> ObtenerArchivosTramite(long idSolicitudTramite);

        /// <summary>
        /// Aprueba un trámite pendiente
        /// </summary>
        Task<bool> AprobarTramite(long idSolicitudTramite);

        /// <summary>
        /// Rechaza un trámite con un motivo especificado
        /// </summary>
        Task<bool> RechazarTramite(long idSolicitudTramite, string motivo);

        /// <summary>
        /// Obtiene un resumen del dashboard del funcionario
        /// </summary>
        Task<ResumenDashboardFuncionarioDTO> ObtenerResumenDashboard();

        #endregion
    }
}

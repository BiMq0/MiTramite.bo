using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace WAMiTramiteGestion.Services
{
    public interface ITramiteService
    {
        #region Métodos de Funcionario

        /// <summary>
        /// Obtiene todos los trámites asignados al funcionario
        /// </summary>
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario);

        /// <summary>
        /// Obtiene los detalles completos de un trámite específico
        /// </summary>
        Task<SolicitudTramiteRegistroDTO?> ObtenerTramitePorIdAsync(long idSolicitudTramite);

        /// <summary>
        /// Completa un trámite (aprobación)
        /// </summary>
        Task<bool> CompletarTramiteAsync(long idSolicitudTramite);

        /// <summary>
        /// Rechaza un trámite con un motivo especificado
        /// </summary>
        Task<bool> RechazarTramiteAsync(long idSolicitudTramite, RechazoTramiteDTO rechazoDto);

        #endregion

        #region Métodos de Gerente

        /// <summary>
        /// Obtiene todos los trámites del sistema (vista de gerente)
        /// </summary>
        Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync();

        #endregion
    }
}

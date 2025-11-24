using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TramiteRep;
using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.TramiteSvc
{
    public class TipoTramiteService : ITipoTramiteService
    {
        private readonly ITipoTramiteRepository _repository;

        public TipoTramiteService(ITipoTramiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TipoTramiteDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerTodosAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipos de trámite", ex);
            }
        }

        public async Task<TipoTramiteDTO> ObtenerPorIdAsync(int idTipoTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerPorIdAsync(idTipoTramite, cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipo de trámite", ex);
            }
        }
    }
}

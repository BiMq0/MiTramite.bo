using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep;
using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public class TipoArchivoService : ITipoArchivoService
    {
        private readonly ITipoArchivoRepository _repository;

        public TipoArchivoService(ITipoArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TipoArchivoParaSubirDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerTodosAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipos de archivo", ex);
            }
        }

        public async Task<TipoArchivoParaSubirDTO> ObtenerPorIdAsync(int idTipoArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerPorIdAsync(idTipoArchivo, cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipo de archivo", ex);
            }
        }
    }
}

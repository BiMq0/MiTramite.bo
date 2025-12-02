using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep;
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _repository;

        public ArchivoService(IArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ArchivoRegistroDTO>> ObtenerDocumentosRentistaAsync(long idRentista, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerDocumentosRentistaAsync((int)idRentista, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener documentos del rentista", ex);
            }
        }

        public async Task<bool> SubirDocumentoAsync(long idRentista, int idTipoArchivo, string nombreArchivo, byte[] contenido, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.SubirDocumentoAsync((int)idRentista, idTipoArchivo, nombreArchivo, contenido, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al subir documento", ex);
            }
        }

        public async Task<bool> EliminarDocumentoAsync(long idRentista, long idDocumento, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.EliminarDocumentoAsync((int)idRentista, idDocumento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar documento", ex);
            }
        }

        public async Task<bool> ExisteDocumentoAsync(long idRentista, int idTipoArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ExisteDocumentoAsync((int)idRentista, idTipoArchivo, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al verificar existencia del documento", ex);
            }
        }

        public async Task<List<MiTramite_Shared.DTOs.ArchivosRequeridosTramite.ArchivosRequeridosTramiteDTO>> ObtenerArchivosRequeridosAsync(int idTipoTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerArchivosRequeridosAsync(idTipoTramite, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener archivos requeridos", ex);
            }
        }
    }
}

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

        public async Task<List<ArchivoRegistroDTO>> ObtenerDocumentosRentistaAsync(int idRentista, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerDocumentosRentistaAsync(idRentista, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener documentos del rentista", ex);
            }
        }

        public async Task<bool> SubirDocumentoAsync(int idRentista, int idTipoArchivo, string nombreArchivo, byte[] contenido, CancellationToken cancellationToken = default)
        {
            try
            {
                var existe = await ExisteDocumentoAsync(idRentista, idTipoArchivo, cancellationToken);
                if (existe)
                    throw new InvalidOperationException($"Ya existe un documento de este tipo para el rentista");

                return await _repository.SubirDocumentoAsync(idRentista, idTipoArchivo, nombreArchivo, contenido, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al subir el documento", ex);
            }
        }

        public async Task<bool> EliminarDocumentoAsync(int idRentista, long idDocumento, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.EliminarDocumentoAsync(idRentista, idDocumento, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el documento", ex);
            }
        }

        public async Task<bool> ExisteDocumentoAsync(int idRentista, int idTipoArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ExisteDocumentoAsync(idRentista, idTipoArchivo, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al verificar existencia del documento", ex);
            }
        }
    }
}

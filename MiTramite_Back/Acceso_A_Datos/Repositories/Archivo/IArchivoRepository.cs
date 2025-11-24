using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public interface IArchivoRepository
    {
        Task<List<ArchivoRegistroDTO>> ObtenerDocumentosRentistaAsync(int idRentista, CancellationToken cancellationToken = default);
        Task<bool> SubirDocumentoAsync(int idRentista, int idTipoArchivo, string nombreArchivo, byte[] contenido, CancellationToken cancellationToken = default);
        Task<bool> EliminarDocumentoAsync(int idRentista, long idDocumento, CancellationToken cancellationToken = default);
        Task<bool> ExisteDocumentoAsync(int idRentista, int idTipoArchivo, CancellationToken cancellationToken = default);
    }
}

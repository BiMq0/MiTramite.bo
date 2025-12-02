using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public interface IArchivoService
    {
        Task<List<ArchivoRegistroDTO>> ObtenerDocumentosRentistaAsync(long idRentista, CancellationToken cancellationToken = default);
        Task<bool> SubirDocumentoAsync(long idRentista, int idTipoArchivo, string nombreArchivo, byte[] contenido, CancellationToken cancellationToken = default);
        Task<bool> EliminarDocumentoAsync(long idRentista, long idDocumento, CancellationToken cancellationToken = default);
        Task<bool> ExisteDocumentoAsync(long idRentista, int idTipoArchivo, CancellationToken cancellationToken = default);
        Task<List<MiTramite_Shared.DTOs.ArchivosRequeridosTramite.ArchivosRequeridosTramiteDTO>> ObtenerArchivosRequeridosAsync(int idTipoTramite, CancellationToken cancellationToken = default);
    }
}

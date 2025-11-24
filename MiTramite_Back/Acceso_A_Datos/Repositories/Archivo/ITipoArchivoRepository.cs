using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public interface ITipoArchivoRepository
    {
        Task<List<TipoArchivoParaSubirDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        Task<TipoArchivoParaSubirDTO> ObtenerPorIdAsync(int idTipoArchivo, CancellationToken cancellationToken = default);
    }
}

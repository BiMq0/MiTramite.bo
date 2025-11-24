using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public interface ITipoArchivoService
    {
        Task<List<TipoArchivoParaSubirDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        Task<TipoArchivoParaSubirDTO> ObtenerPorIdAsync(int idTipoArchivo, CancellationToken cancellationToken = default);
    }
}

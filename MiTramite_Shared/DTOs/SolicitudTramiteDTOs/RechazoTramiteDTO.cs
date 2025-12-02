using System.Collections.Generic;

namespace MiTramite_Shared.DTOs.SolicitudTramiteDTOs
{
    public class RechazoTramiteDTO
    {
        public string Motivo { get; set; } = string.Empty;
        public string AccionParaRealizar { get; set; } = string.Empty;
        public List<ArchivoRechazadoDTO> ArchivosErroneos { get; set; } = new List<ArchivoRechazadoDTO>();
    }

    public class ArchivoRechazadoDTO
    {
        public long IdArchivo { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}

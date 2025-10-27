namespace MiTramite_Domain.Entities;

public class ArchivosRequeridosTramite
{
    public int IdTipoTramite { get; set; }
    public int IdTipoArchivo { get; set; }

    public TipoTramite TipoTramite { get; set; } = null!;
    public TipoArchivo TipoArchivo { get; set; } = null!;
}

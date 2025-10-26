using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities;

public class TipoArchivo
{
    [Key]
    public int IdTipoArchivo { get; set; }
    public string Nombre { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public int PesoMaximoMB { get; set; }
}

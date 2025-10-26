using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities;

public class Opcion
{
    [Key]
    public int IdOpcion { get; set; }
    public string LabelOpcion { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using MiTramite_Domain.Constants;
using MiTramite_Domain.Entities;

namespace MiTramite_Domain.Entities;

public class Rentista
{
    [Key]
    public long IdRentista { get; set; }
    public string CI { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string ApellidoPaterno { get; set; } = null!;
    public string ApellidoMaterno { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    public string Correo { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ActivityStatus Activo { get; set; } = ActivityStatus.Activo;

    public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();
}

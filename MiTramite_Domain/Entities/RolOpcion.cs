namespace MiTramite_Domain.Entities;

public class RolOpcion
{
    public int IdRol { get; set; }
    public int IdOpcion { get; set; }
    public Rol? Rol { get; set; }
    public Opcion? Opcion { get; set; }
}

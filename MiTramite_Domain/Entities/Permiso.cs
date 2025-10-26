using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}

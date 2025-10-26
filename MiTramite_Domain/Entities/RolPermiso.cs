using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTramite_Domain.Entities
{
    public class RolPermiso
    {
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }
        public Rol? Rol { get; set; }
        public Permiso? Permiso { get; set; }

    }
}

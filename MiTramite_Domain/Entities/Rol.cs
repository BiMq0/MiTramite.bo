using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public decimal Salario { get; set; }
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
        public ICollection<RolOpcion> RolOpciones { get; set; } = new List<RolOpcion>();
        public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}

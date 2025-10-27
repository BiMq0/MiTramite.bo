using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities
{
    public class EstadoTramite
    {
        [Key]
        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}

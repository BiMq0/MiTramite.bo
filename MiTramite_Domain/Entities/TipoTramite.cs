using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities
{
    public class TipoTramite
    {
        [Key]
        public int IdTipoTramite { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int DiasDuracionEstimada { get; set; }
        public int Importancia { get; set; }
    }
}

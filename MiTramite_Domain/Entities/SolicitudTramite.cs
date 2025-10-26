using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MiTramite_Domain.Entities
{
    public class SolicitudTramite
    {
        [Key]
        public long IdTramite { get; set; }
        public int IdTipoTramite { get; set; }
        public long IdRentista { get; set; }
        public long IdFuncionario { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdEstado { get; set; }
        public bool Reasignado { get; set; }

        public TipoTramite TipoTramite { get; set; } = null!;
        public Rentista? Rentista { get; set; }
        public Funcionario? Funcionario { get; set; }
        public EstadoTramite EstadoTramite { get; set; } = null!;
    }
}

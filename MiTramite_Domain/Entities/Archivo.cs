using System.ComponentModel.DataAnnotations;
using MiTramite_Domain.Constants;

namespace MiTramite_Domain.Entities
{
    public class Archivo
    {
        [Key]
        public long IdArchivo { get; set; }

        public long IdRentista { get; set; }

        public int IdTipoArchivo { get; set; }

        public string Nombre { get; set; } = null!;

        public byte[] Contenido { get; set; } = null!;

        public int Peso { get; set; }

        public ActivityStatus Activo { get; set; } = ActivityStatus.Activo;


        #region Navigation Properties
        public Rentista? Rentista { get; set; }
        public TipoArchivo? TipoArchivo { get; set; }
        #endregion

    }
}

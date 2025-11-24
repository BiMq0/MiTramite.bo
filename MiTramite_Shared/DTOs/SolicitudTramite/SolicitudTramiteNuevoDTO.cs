using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Constants;

namespace MiTramite_Shared.DTOs.SolicitudTramiteDTOs
{
    public class SolicitudTramiteNuevoDTO
    {
        public int IdTipoTramite { get; set; }
        public long IdRentista { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        public SolicitudTramiteNuevoDTO()
        {

        }
    }
}
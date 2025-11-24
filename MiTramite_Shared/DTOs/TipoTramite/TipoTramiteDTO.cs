using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.TipoTramiteDTOs
{
    public class TipoTramiteDTO
    {
        public int IdTipoTramite { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DiasDuracionEstimada { get; set; }
        public TipoTramiteDTO(TipoTramite tipoTramite)
        {
            IdTipoTramite = tipoTramite.IdTipoTramite;
            Nombre = tipoTramite.Nombre;
            Descripcion = tipoTramite.Descripcion;
            DiasDuracionEstimada = tipoTramite.DiasDuracionEstimada;
        }
        public TipoTramiteDTO()
        {

        }
    }
}
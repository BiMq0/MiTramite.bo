using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.TipoArchivoDTOs
{
    public class TipoArchivoParaSubirDTO
    {
        public int IdTipoArchivo { get; set; }
        public string Nombre { get; set; }
        public int PesoMaximoMB { get; set; }
        public TipoArchivoParaSubirDTO(TipoArchivo tipoArchivo)
        {
            IdTipoArchivo = tipoArchivo.IdTipoArchivo;
            Nombre = tipoArchivo.Nombre;
            PesoMaximoMB = tipoArchivo.PesoMaximoMB;
        }
        public TipoArchivoParaSubirDTO()
        {

        }
    }
}
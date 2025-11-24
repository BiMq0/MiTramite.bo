using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiTramite_Shared.DTOs.ArchivoDTOs
{
    public class ArchivoNuevoDTO
    {
        public int IdRentista { get; set; }
        public int IdTipoArchivo { get; set; }
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public int Peso { get; set; }
        public ArchivoNuevoDTO()
        {

        }
    }
}
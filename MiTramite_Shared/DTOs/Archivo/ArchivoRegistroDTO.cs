using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Constants;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.ArchivoDTOs
{
    public class ArchivoRegistroDTO
    {
        public long IdArchivo { get; set; }
        public long IdRentista { get; set; }
        public int IdTipoArchivo { get; set; }
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public int Peso { get; set; }
        public ArchivoRegistroDTO(Archivo archivo)
        {
            IdArchivo = archivo.IdArchivo;
            IdRentista = archivo.IdRentista;
            IdTipoArchivo = archivo.IdTipoArchivo;
            Nombre = archivo.Nombre;
            Contenido = archivo.Contenido;
            Peso = archivo.Peso;
        }
        public ArchivoRegistroDTO()
        {

        }
    }
}
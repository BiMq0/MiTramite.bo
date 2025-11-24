using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.RentistaDTOs
{
    public class RentistaCurrentDataDTO
    {
        public long IdRentista { get; set; }
        public string CI { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public RentistaCurrentDataDTO(Rentista rentista)
        {
            IdRentista = rentista.IdRentista;
            CI = rentista.CI;
            Nombres = rentista.Nombres;
            ApellidoPaterno = rentista.ApellidoPaterno;
            ApellidoMaterno = rentista.ApellidoMaterno;
            Correo = rentista.Correo;
        }

        public RentistaCurrentDataDTO()
        {

        }
    }
}
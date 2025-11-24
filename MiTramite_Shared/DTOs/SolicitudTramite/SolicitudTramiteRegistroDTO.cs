using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.SolicitudTramiteDTOs
{
    public class SolicitudTramiteRegistroDTO
    {
        public long IdSolicitudTramite { get; set; }
        public string NombreTipoTramite { get; set; }
        public string NombreRentista { get; set; }
        public string NombreFuncionarioAsignado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string Estado { get; set; }
        public string Reassigned { get; set; }

        public SolicitudTramiteRegistroDTO(SolicitudTramite solicitudTramite)
        {

            IdSolicitudTramite = solicitudTramite.IdSolicitudTramite;
            NombreTipoTramite = solicitudTramite.TipoTramite.Nombre;
            NombreRentista = solicitudTramite.Rentista!.Nombres;
            NombreFuncionarioAsignado = solicitudTramite.Funcionario!.Nombres;
            FechaSolicitud = solicitudTramite.FechaSolicitud;
            FechaEstimadaEntrega = solicitudTramite.FechaEstimadaEntrega;
            Estado = solicitudTramite.EstadoTramite.Nombre;
            Reassigned = solicitudTramite.Reasignado ? "Sí" : "No";
        }
        public SolicitudTramiteRegistroDTO()
        {

        }
    }
}
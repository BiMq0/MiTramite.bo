using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Domain.Constants;

namespace MiTramite_Shared.DTOs.SolicitudTramiteDTOs
{
    public class SolicitudTramiteRegistroDTO
    {
        public long IdSolicitudTramite { get; set; }
        public string NombreTipoTramite { get; set; }
        public string NombreCompletoRentista { get; set; }
        public string NombreCompletoFuncionarioAsignado { get; set; }
        public string CorreoRentista { get; set; }
        public string CorreoFuncionarioAsignado { get; set; }
        public long IdRentista { get; set; }
        public long IdTipoTramite { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string MotivoRechazo { get; set; } = "";
        public string Estado { get; set; }
        public string Reassigned { get; set; }

        public SolicitudTramiteRegistroDTO(SolicitudTramite solicitudTramite)
        {

            IdSolicitudTramite = solicitudTramite.IdSolicitudTramite;
            NombreTipoTramite = solicitudTramite.TipoTramite.Nombre;
            IdRentista = solicitudTramite.Rentista!.IdRentista;
            IdTipoTramite = solicitudTramite.TipoTramite.IdTipoTramite;
            NombreCompletoRentista = $"{solicitudTramite.Rentista!.Nombres} {solicitudTramite.Rentista!.ApellidoPaterno} {solicitudTramite.Rentista!.ApellidoMaterno ?? ""}";
            NombreCompletoFuncionarioAsignado = $"{solicitudTramite.Funcionario!.Nombres} {solicitudTramite.Funcionario!.ApellidoPaterno} {solicitudTramite.Funcionario!.ApellidoMaterno ?? ""}";
            FechaSolicitud = solicitudTramite.FechaSolicitud;
            FechaEstimadaEntrega = solicitudTramite.FechaEstimadaEntrega;
            Estado = solicitudTramite.EstadoTramite.Nombre;
            Reassigned = solicitudTramite.Reasignado ? "Sí" : "No";
            CorreoRentista = solicitudTramite.Rentista!.Correo;
            CorreoFuncionarioAsignado = solicitudTramite.Funcionario!.Correo;
            MotivoRechazo = solicitudTramite.EstadoTramite.IdEstado == (int)TramiteEstados.Rechazado ? solicitudTramite.MotivoRechazo ?? "" : "";
        }
        public SolicitudTramiteRegistroDTO()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MiTramite_Domain.Constants;

namespace MiTramite_Domain.Entities
{
    public class Funcionario
    {
        [Key]
        public long IdFuncionario { get; set; }
        public string CodigoFuncionario { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public int? PesoDisponibilidad { get; set; }
        public string Correo { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int IdRol { get; set; }
        public ActivityStatus Activo { get; set; } = ActivityStatus.Activo;


        public Rol? Rol { get; set; }
        public ICollection<SolicitudTramite> SolicitudesAsignadas { get; set; } = new List<SolicitudTramite>();
        public ICollection<Incumplimiento> Incumplimiento { get; set; } = new List<Incumplimiento>();
    }
}

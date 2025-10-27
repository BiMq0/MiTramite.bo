using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities;

public class Incumplimiento
{
    public long IdSolicitudTramite { get; set; }
    public long IdFuncionario { get; set; }
    public long IdFuncionarioReasignado { get; set; }
    public DateTime FechaReasignacion { get; set; }


    public SolicitudTramite? SolicitudTramite { get; set; }
    public Funcionario? Funcionario { get; set; }
    public Funcionario? FuncionarioReasignado { get; set; }
}

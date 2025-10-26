using System.ComponentModel.DataAnnotations;

namespace MiTramite_Domain.Entities;

public class Incumplimiento
{
    public long IdTramite { get; set; }
    public long IdFuncionario { get; set; }
    public long IdFuncionarioReasignado { get; set; }
    public DateTime FechaReasignacion { get; set; }
}

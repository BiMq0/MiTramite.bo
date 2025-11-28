using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep
{
    public interface IIncumplimientoRepository
    {
        Task<bool> RegistrarIncumplimiento(SolicitudTramite tramite, long idFuncionarioReasignado);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep
{
    public class IncumplimientoRepository : IIncumplimientoRepository
    {

        private readonly MiTramiteDbContext _context;
        public IncumplimientoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegistrarIncumplimiento(SolicitudTramite tramite, long idFuncionarioReasignado)
        {
            var incumplimiento = new Incumplimiento
            {
                IdSolicitudTramite = tramite.IdSolicitudTramite,
                FechaReasignacion = DateTime.UtcNow,
                IdFuncionario = tramite.IdFuncionario,
                IdFuncionarioReasignado = idFuncionarioReasignado,
            };

            if (_context.Incumplimientos.FirstOrDefault(i => i.IdSolicitudTramite == tramite.IdSolicitudTramite && i.IdFuncionarioReasignado == idFuncionarioReasignado && i.IdFuncionario == tramite.IdFuncionario) == null)
            {
                _context.Incumplimientos.Add(incumplimiento);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
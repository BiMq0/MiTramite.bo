using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

            if (await _context.Incumplimientos.FirstOrDefaultAsync(i => i.IdSolicitudTramite == tramite.IdSolicitudTramite && i.IdFuncionarioReasignado == idFuncionarioReasignado && i.IdFuncionario == tramite.IdFuncionario) == null)
            {
                _context.Incumplimientos.Add(incumplimiento);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Incumplimiento>> ObtenerTodosAsync()
        {
            return await _context.Incumplimientos
                .Include(i => i.SolicitudTramite)
                    .ThenInclude(st => st.Rentista)
                .Include(i => i.SolicitudTramite)
                    .ThenInclude(st => st.TipoTramite)
                .Include(i => i.Funcionario)
                .Include(i => i.FuncionarioReasignado)
                .OrderByDescending(i => i.FechaReasignacion)
                .ToListAsync();
        }

        public async Task<Incumplimiento?> ObtenerPorTramiteIdAsync(long idSolicitudTramite)
        {
            return await _context.Incumplimientos
                .Include(i => i.SolicitudTramite)
                    .ThenInclude(st => st.Rentista)
                .Include(i => i.SolicitudTramite)
                    .ThenInclude(st => st.TipoTramite)
                .Include(i => i.Funcionario)
                .Include(i => i.FuncionarioReasignado)
                .FirstOrDefaultAsync(i => i.IdSolicitudTramite == idSolicitudTramite);
        }
    }
}

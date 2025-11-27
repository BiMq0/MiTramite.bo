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
        public async Task<bool> RegistrarIncumplimiento(SolicitudTramite tramite)
        {
            // Lógica para registrar el incumplimiento en la base de datos
            return true;
        }
    }
}
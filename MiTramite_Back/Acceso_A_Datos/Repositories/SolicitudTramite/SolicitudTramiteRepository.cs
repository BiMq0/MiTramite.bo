using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramiteRep
{
    public class SolicitudTramiteRepository : ISolicitudTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public SolicitudTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }
    }
}

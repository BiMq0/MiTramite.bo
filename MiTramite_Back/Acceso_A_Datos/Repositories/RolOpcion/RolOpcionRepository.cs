using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.RolOpcionRep
{
    public class RolOpcionRepository : IRolOpcionRepository
    {
        private readonly MiTramiteDbContext _context;

        public RolOpcionRepository(MiTramiteDbContext context)
        {
            _context = context;
        }
    }
}

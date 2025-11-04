using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Acceso_A_Datos.Repositories.OpcionRep
{
    public class OpcionRepository : IOpcionRepository
    {
        private readonly MiTramiteDbContext _context;
        public OpcionRepository(MiTramiteDbContext context) => _context = context;
    }
}

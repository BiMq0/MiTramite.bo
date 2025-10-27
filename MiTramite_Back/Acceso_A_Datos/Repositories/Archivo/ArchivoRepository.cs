using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly MiTramiteDbContext _context;

        public ArchivoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Archivo>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Archivos.ToListAsync(cancellationToken);

        public async Task<Archivo?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.Archivos.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Archivo entity, CancellationToken cancellationToken = default)
        {
            await _context.Archivos.AddAsync(entity, cancellationToken);
        }

        public void Update(Archivo entity)
        {
            _context.Archivos.Update(entity);
        }

        public void Remove(Archivo entity)
        {
            _context.Archivos.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}

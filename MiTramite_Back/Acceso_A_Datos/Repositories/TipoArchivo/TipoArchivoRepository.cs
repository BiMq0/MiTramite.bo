using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Acceso_A_Datos.Repositories.TipoArchivoRep
{
    public class TipoArchivoRepository : ITipoArchivoRepository
    {
        private readonly MiTramiteDbContext _context;

        public TipoArchivoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoArchivo>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.TipoArchivos.ToListAsync(cancellationToken);

        public async Task<TipoArchivo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.TipoArchivos.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(TipoArchivo entity, CancellationToken cancellationToken = default)
        {
            await _context.TipoArchivos.AddAsync(entity, cancellationToken);
        }

        public void Update(TipoArchivo entity)
        {
            _context.TipoArchivos.Update(entity);
        }

        public void Remove(TipoArchivo entity)
        {
            _context.TipoArchivos.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}

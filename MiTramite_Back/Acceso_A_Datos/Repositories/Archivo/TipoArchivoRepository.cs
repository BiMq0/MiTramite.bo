using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Shared.DTOs.TipoArchivoDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public class TipoArchivoRepository : ITipoArchivoRepository
    {
        private readonly MiTramiteDbContext _context;

        public TipoArchivoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoArchivoParaSubirDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var tiposArchivo = await _context.TipoArchivos
                    .Select(ta => new TipoArchivoParaSubirDTO
                    {
                        IdTipoArchivo = ta.IdTipoArchivo,
                        Nombre = ta.Nombre,
                        PesoMaximoMB = ta.PesoMaximoMB
                    })
                    .ToListAsync(cancellationToken);

                return tiposArchivo;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipos de archivo", ex);
            }
        }

        public async Task<TipoArchivoParaSubirDTO> ObtenerPorIdAsync(int idTipoArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                var tipoArchivo = await _context.TipoArchivos
                    .Where(ta => ta.IdTipoArchivo == idTipoArchivo)
                    .Select(ta => new TipoArchivoParaSubirDTO
                    {
                        IdTipoArchivo = ta.IdTipoArchivo,
                        Nombre = ta.Nombre,
                        PesoMaximoMB = ta.PesoMaximoMB
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (tipoArchivo == null)
                    throw new KeyNotFoundException($"Tipo de archivo {idTipoArchivo} no encontrado");

                return tipoArchivo;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipo de archivo", ex);
            }
        }
    }
}

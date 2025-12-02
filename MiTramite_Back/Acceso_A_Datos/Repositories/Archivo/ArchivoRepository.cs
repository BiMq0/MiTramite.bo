using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Shared.DTOs.ArchivoDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly MiTramiteDbContext _context;

        public ArchivoRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArchivoRegistroDTO>> ObtenerDocumentosRentistaAsync(int idRentista, CancellationToken cancellationToken = default)
        {
            try
            {
                var documentos = await _context.Archivos
                    .Where(a => a.IdRentista == idRentista)
                    .Select(a => new ArchivoRegistroDTO
                    {
                        IdArchivo = a.IdArchivo,
                        IdRentista = a.IdRentista,
                        IdTipoArchivo = a.IdTipoArchivo,
                        Nombre = a.Nombre,
                        Contenido = a.Contenido,
                        Peso = a.Peso
                    })
                    .ToListAsync(cancellationToken);

                return documentos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener documentos del rentista", ex);
            }
        }

        public async Task<bool> SubirDocumentoAsync(int idRentista, int idTipoArchivo, string nombreArchivo, byte[] contenido, CancellationToken cancellationToken = default)
        {
            try
            {
                var archivo = new MiTramite_Domain.Entities.Archivo
                {
                    IdRentista = idRentista,
                    IdTipoArchivo = idTipoArchivo,
                    Nombre = nombreArchivo,
                    Contenido = contenido,
                    Peso = contenido.Length
                };

                await _context.Archivos.AddAsync(archivo, cancellationToken);
                var result = await _context.SaveChangesAsync(cancellationToken);

                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error al subir el documento", ex);
            }
        }

        public async Task<bool> EliminarDocumentoAsync(int idRentista, long idDocumento, CancellationToken cancellationToken = default)
        {
            try
            {
                var archivo = await _context.Archivos
                    .FirstOrDefaultAsync(a => a.IdArchivo == idDocumento && a.IdRentista == idRentista, cancellationToken);

                if (archivo == null)
                    return false;

                _context.Archivos.Remove(archivo);
                var result = await _context.SaveChangesAsync(cancellationToken);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el documento", ex);
            }
        }

        public async Task<bool> ExisteDocumentoAsync(int idRentista, int idTipoArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                var existe = await _context.Archivos
                    .AnyAsync(a => a.IdRentista == idRentista && a.IdTipoArchivo == idTipoArchivo, cancellationToken);

                return existe;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al verificar existencia del documento", ex);
            }
        }

        public async Task<List<MiTramite_Shared.DTOs.ArchivosRequeridosTramite.ArchivosRequeridosTramiteDTO>> ObtenerArchivosRequeridosAsync(int idTipoTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                var archivosRequeridos = await _context.ArchivosRequeridosTramites
                    .Include(art => art.TipoArchivo)
                    .Where(art => art.IdTipoTramite == idTipoTramite)
                    .Select(art => new MiTramite_Shared.DTOs.ArchivosRequeridosTramite.ArchivosRequeridosTramiteDTO
                    {
                        IdTipoArchivo = art.IdTipoArchivo,
                        Nombre = art.TipoArchivo.Nombre,
                        Extension = art.TipoArchivo.Extension,
                        PesoMaximoMB = art.TipoArchivo.PesoMaximoMB
                    })
                    .ToListAsync(cancellationToken);

                return archivosRequeridos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener archivos requeridos", ex);
            }
        }
    }
}

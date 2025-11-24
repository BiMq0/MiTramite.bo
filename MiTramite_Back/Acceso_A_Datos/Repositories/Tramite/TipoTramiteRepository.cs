using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Shared.DTOs.TipoTramiteDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.TramiteRep
{
    public class TipoTramiteRepository : ITipoTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public TipoTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoTramiteDTO>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var tiposTramite = await _context.TipoTramites
                    .Select(tt => new TipoTramiteDTO
                    {
                        IdTipoTramite = tt.IdTipoTramite,
                        Nombre = tt.Nombre,
                        Descripcion = tt.Descripcion,
                        DiasDuracionEstimada = tt.DiasDuracionEstimada
                    })
                    .ToListAsync(cancellationToken);

                return tiposTramite;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipos de trámite", ex);
            }
        }

        public async Task<TipoTramiteDTO> ObtenerPorIdAsync(int idTipoTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                var tipoTramite = await _context.TipoTramites
                    .Where(tt => tt.IdTipoTramite == idTipoTramite)
                    .Select(tt => new TipoTramiteDTO
                    {
                        IdTipoTramite = tt.IdTipoTramite,
                        Nombre = tt.Nombre,
                        Descripcion = tt.Descripcion,
                        DiasDuracionEstimada = tt.DiasDuracionEstimada
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (tipoTramite == null)
                    throw new KeyNotFoundException($"Tipo de trámite {idTipoTramite} no encontrado");

                return tipoTramite;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener tipo de trámite", ex);
            }
        }
    }
}

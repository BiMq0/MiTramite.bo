using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.Reportes
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly MiTramiteDbContext _context;

        public ReporteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteTramitesPorMesDTO>> GetTramitesPorMesAsync(int year)
        {
            var data = await _context.SolicitudTramites
                .Where(t => t.FechaSolicitud.Year == year)
                .GroupBy(t => t.FechaSolicitud.Month)
                .Select(g => new { Mes = g.Key, Cantidad = g.Count() })
                .ToListAsync();

            var result = new List<ReporteTramitesPorMesDTO>();
            string[] meses = { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            for (int i = 1; i <= 12; i++)
            {
                var item = data.FirstOrDefault(d => d.Mes == i);
                result.Add(new ReporteTramitesPorMesDTO
                {
                    Mes = meses[i - 1],
                    Cantidad = item?.Cantidad ?? 0
                });
            }

            return result;
        }

        public async Task<List<ReporteRentistasPorEdadDTO>> GetRentistasPorEdadAsync()
        {
            var rentistas = await _context.Rentistas.Select(r => r.FechaNacimiento).ToListAsync();
            var hoy = DateTime.Today;

            var edades = rentistas.Select(f =>
            {
                var edad = hoy.Year - f.Year;
                if (f.Date > hoy.AddYears(-edad)) edad--;
                return edad;
            }).ToList();

            return new List<ReporteRentistasPorEdadDTO>
            {
                new ReporteRentistasPorEdadDTO { RangoEdad = "18-25", Cantidad = edades.Count(e => e >= 18 && e <= 25) },
                new ReporteRentistasPorEdadDTO { RangoEdad = "26-35", Cantidad = edades.Count(e => e >= 26 && e <= 35) },
                new ReporteRentistasPorEdadDTO { RangoEdad = "36-50", Cantidad = edades.Count(e => e >= 36 && e <= 50) },
                new ReporteRentistasPorEdadDTO { RangoEdad = "51-65", Cantidad = edades.Count(e => e >= 51 && e <= 65) },
                new ReporteRentistasPorEdadDTO { RangoEdad = "65+", Cantidad = edades.Count(e => e > 65) }
            };
        }

        public async Task<List<ReporteEstadoTramitesDTO>> GetEstadoTramitesAsync()
        {
            return await _context.SolicitudTramites
                .GroupBy(t => t.EstadoTramite.Nombre)
                .Select(g => new ReporteEstadoTramitesDTO
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();
        }

        public async Task<List<ReporteIncumplimientosFuncionarioDTO>> GetIncumplimientosTopAsync()
        {
            return await _context.Incumplimientos
                .Include(i => i.Funcionario)
                .GroupBy(i => i.Funcionario!.Nombres + " " + i.Funcionario.ApellidoPaterno)
                .Select(g => new ReporteIncumplimientosFuncionarioDTO
                {
                    NombreFuncionario = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToListAsync();
        }
    }
}

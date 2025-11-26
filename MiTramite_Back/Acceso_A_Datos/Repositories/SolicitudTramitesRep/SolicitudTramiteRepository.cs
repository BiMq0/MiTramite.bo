using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Domain.Constants;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramitesRep
{
    public class SolicitudTramiteRepository : ISolicitudTramiteRepository
    {
        private readonly MiTramiteDbContext _context;

        public SolicitudTramiteRepository(MiTramiteDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearSolicitudTramiteAsync(SolicitudTramiteNuevoDTO solicitudNueva, CancellationToken cancellationToken = default)
        {
            try
            {
                var rentistaExiste = await _context.Rentistas
                    .AnyAsync(r => r.IdRentista == solicitudNueva.IdRentista, cancellationToken);

                if (!rentistaExiste)
                    throw new KeyNotFoundException($"El rentista con ID {solicitudNueva.IdRentista} no existe");

                var tipoTramiteExiste = await _context.TipoTramites
                    .AnyAsync(tt => tt.IdTipoTramite == solicitudNueva.IdTipoTramite, cancellationToken);

                if (!tipoTramiteExiste)
                    throw new KeyNotFoundException($"El tipo de trámite con ID {solicitudNueva.IdTipoTramite} no existe");

                var funcionario = await _context.Funcionarios
                    .Where(f => f.IdRol == 1)
                    .OrderBy(f => f.PesoDisponibilidad ?? int.MaxValue)
                    .FirstOrDefaultAsync(cancellationToken);

                if (funcionario == null)
                    throw new InvalidOperationException("No hay funcionarios disponibles para asignar");

                var solicitud = new SolicitudTramite
                {
                    IdTipoTramite = solicitudNueva.IdTipoTramite,
                    IdRentista = solicitudNueva.IdRentista,
                    IdFuncionario = funcionario.IdFuncionario,
                    FechaSolicitud = DateTime.UtcNow,
                    IdEstadoTramite = (int)TramiteEstados.Pendiente,
                    Reasignado = false
                };

                _context.SolicitudTramites.Add(solicitud);

                await _context.SaveChangesAsync(cancellationToken);

                var tramitesPendientes = await _context.SolicitudTramites
                    .Include(st => st.TipoTramite)
                    .Where(st => st.IdFuncionario == funcionario.IdFuncionario && st.IdEstadoTramite == (int)TramiteEstados.Pendiente)
                    .ToListAsync(cancellationToken);

                var tramitesConPrioridad = tramitesPendientes
                    .Select(st => new TramitePrioridad
                    {
                        Tramite = st,
                        Importancia = st.TipoTramite.Importancia,
                        DiasRestantes = (st.FechaEstimadaEntrega - DateTime.UtcNow).TotalDays,
                        Prioridad = 0.0
                    })
                    .ToList();

                foreach (var item in tramitesConPrioridad)
                {
                    // Mayor importancia = mayor prioridad
                    // Menos días restantes = mayor urgencia
                    double factorUrgencia = item.DiasRestantes <= 0 ? 10 : Math.Max(1, 5 / (item.DiasRestantes + 1));
                    item.Prioridad = item.Importancia * factorUrgencia;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear la solicitud del trámite", ex);
            }
        }

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorRentistaAsync(long idRentista, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramites = await _context.SolicitudTramites
                    .Include(st => st.TipoTramite)
                    .Include(st => st.Rentista)
                    .Include(st => st.Funcionario)
                    .Include(st => st.EstadoTramite)
                    .Where(st => st.IdRentista == idRentista)
                    .ToListAsync(cancellationToken);

                if (!tramites.Any())
                    return new List<SolicitudTramiteRegistroDTO>();

                return tramites
                    .Select(st => new SolicitudTramiteRegistroDTO(st))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener los trámites del rentista", ex);
            }
        }

        public async Task<SolicitudTramiteRegistroDTO?> ObtenerTramitePorIdAsync(long idSolicitudTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramite = await _context.SolicitudTramites
                    .Include(st => st.TipoTramite)
                    .Include(st => st.Rentista)
                    .Include(st => st.Funcionario)
                    .Include(st => st.EstadoTramite)
                    .FirstOrDefaultAsync(st => st.IdSolicitudTramite == idSolicitudTramite, cancellationToken);

                if (tramite == null)
                    return null;

                return new SolicitudTramiteRegistroDTO(tramite);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener el trámite por ID", ex);
            }
        }

        public async Task<bool> CompletarTramiteAsync(long idSolicitudTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramite = await _context.SolicitudTramites
                    .FirstOrDefaultAsync(st => st.IdSolicitudTramite == idSolicitudTramite, cancellationToken);

                if (tramite == null)
                    return false;

                tramite.IdEstadoTramite = (int)TramiteEstados.Completado;
                _context.SolicitudTramites.Update(tramite);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al completar el trámite", ex);
            }
        }

        public async Task<bool> RechazarTramiteAsync(long idSolicitudTramite, string motivo, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramite = await _context.SolicitudTramites
                    .FirstOrDefaultAsync(st => st.IdSolicitudTramite == idSolicitudTramite, cancellationToken);

                if (tramite == null)
                    return false;

                tramite.IdEstadoTramite = (int)TramiteEstados.Rechazado;
                _context.SolicitudTramites.Update(tramite);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al rechazar el trámite", ex);
            }
        }

        public async Task<(string CorreoRentista, string CorreoFuncionario, string NombreRentista, string NombreFuncionario, string NombreTramite)?> ObtenerCorreosTramiteAsync(long idSolicitudTramite, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramite = await _context.SolicitudTramites
                    .Include(st => st.Rentista)
                    .Include(st => st.Funcionario)
                    .Include(st => st.TipoTramite)
                    .FirstOrDefaultAsync(st => st.IdSolicitudTramite == idSolicitudTramite, cancellationToken);

                if (tramite == null)
                    return null;

                return (
                    CorreoRentista: tramite.Rentista?.Correo ?? "",
                    CorreoFuncionario: tramite.Funcionario?.Correo ?? "",
                    NombreRentista: tramite.Rentista?.Nombres ?? "",
                    NombreFuncionario: tramite.Funcionario?.Nombres ?? "",
                    NombreTramite: tramite.TipoTramite?.Nombre ?? ""
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener correos del trámite", ex);
            }
        }

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                var tramites = await _context.SolicitudTramites
                    .Include(st => st.TipoTramite)
                    .Include(st => st.Rentista)
                    .Include(st => st.Funcionario)
                    .Include(st => st.EstadoTramite)
                    .Where(st => st.IdFuncionario == idFuncionario)
                    .OrderByDescending(st => st.FechaSolicitud)
                    .ToListAsync(cancellationToken);

                return tramites.Select(st => new SolicitudTramiteRegistroDTO(st)).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener trámites del funcionario", ex);
            }
        }

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var tramites = await _context.SolicitudTramites
                    .Include(st => st.TipoTramite)
                    .Include(st => st.Rentista)
                    .Include(st => st.Funcionario)
                    .Include(st => st.EstadoTramite)
                    .OrderByDescending(st => st.FechaSolicitud)
                    .ToListAsync(cancellationToken);

                return tramites.Select(st => new SolicitudTramiteRegistroDTO(st)).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener todos los trámites", ex);
            }
        }

        private class TramitePrioridad
        {
            public SolicitudTramite Tramite { get; set; }
            public int Importancia { get; set; }
            public double DiasRestantes { get; set; }
            public double Prioridad { get; set; }
        }
    }
}
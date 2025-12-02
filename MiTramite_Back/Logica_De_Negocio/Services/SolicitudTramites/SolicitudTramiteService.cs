using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramitesRep;
using MiTramite_Back.Logica_De_Negocio.Services.EmailSvc;
using MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

namespace MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramitesSvc
{
    public class SolicitudTramiteService : ISolicitudTramiteService
    {
        private readonly ISolicitudTramiteRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IArchivoService _archivoService;

        public SolicitudTramiteService(ISolicitudTramiteRepository repository, IEmailService emailService, IArchivoService archivoService)
        {
            _repository = repository;
            _emailService = emailService;
            _archivoService = archivoService;
        }

        public async Task<bool> CrearSolicitudTramiteAsync(SolicitudTramiteNuevoDTO solicitudNueva, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.CrearSolicitudTramiteAsync(solicitudNueva, cancellationToken);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
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
                return await _repository.ObtenerTramitesPorRentistaAsync(idRentista, cancellationToken);
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
                return await _repository.ObtenerTramitePorIdAsync(idSolicitudTramite, cancellationToken);
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
                var resultado = await _repository.CompletarTramiteAsync(idSolicitudTramite, cancellationToken);

                if (resultado)
                {
                    var correos = await _repository.ObtenerCorreosTramiteAsync(idSolicitudTramite, cancellationToken);
                    if (correos.HasValue)
                    {
                        await _emailService.NotificarCompletacionTramiteAsync(
                            correos.Value.CorreoRentista,
                            correos.Value.NombreRentista,
                            correos.Value.NombreTramite,
                            correos.Value.CorreoFuncionario,
                            cancellationToken
                        );
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al completar el trámite", ex);
            }
        }

        public async Task<bool> RechazarTramiteAsync(long idSolicitudTramite, RechazoTramiteDTO rechazoDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var resultado = await _repository.RechazarTramiteAsync(idSolicitudTramite, rechazoDto.Motivo, cancellationToken);

                if (resultado)
                {
                    var correos = await _repository.ObtenerCorreosTramiteAsync(idSolicitudTramite, cancellationToken);
                    
                    // Eliminar archivos erróneos
                    if (rechazoDto.ArchivosErroneos != null && rechazoDto.ArchivosErroneos.Any() && correos.HasValue)
                    {
                        var tramite = await _repository.ObtenerTramitePorIdAsync(idSolicitudTramite, cancellationToken);
                        if (tramite != null)
                        {
                            foreach (var archivo in rechazoDto.ArchivosErroneos)
                            {
                                await _archivoService.EliminarDocumentoAsync(tramite.IdRentista, archivo.IdArchivo, cancellationToken);
                            }
                        }
                    }

                    if (correos.HasValue)
                    {
                        var nombresArchivos = rechazoDto.ArchivosErroneos?.Select(a => a.Nombre).ToList();
                        await _emailService.NotificarRechazoTramiteAsync(
                            correos.Value.CorreoRentista,
                            correos.Value.NombreRentista,
                            correos.Value.NombreTramite,
                            rechazoDto.Motivo,
                            correos.Value.CorreoFuncionario,
                            rechazoDto.AccionParaRealizar,
                            nombresArchivos,
                            cancellationToken
                        );
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al rechazar el trámite", ex);
            }
        }

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTramitesPorFuncionarioAsync(long idFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerTramitesPorFuncionarioAsync(idFuncionario, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener los trámites del funcionario", ex);
            }
        }

        public async Task<List<SolicitudTramiteRegistroDTO>> ObtenerTodosLosTramitesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.ObtenerTodosLosTramitesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener todos los trámites", ex);
            }
        }
    }
}
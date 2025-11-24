// #region DATOS DE EJEMPLO TEMPORALES - ELIMINAR CUANDO SE TENGAN LOS DTOS REALES
// Este archivo contiene clases DTO de ejemplo para permitir compilación y pruebas
// con datos hardcodeados. Se debe eliminar cuando los DTOs reales estén disponibles
// en MiTramite_Shared.DTOs.TramiteDTOs

namespace MiTramite_Front.Services.Tramite;

/// <summary>
/// DTO temporal para listar trámites
/// </summary>
public class SolicitudTramiteDTO
{
    public long IdSolicitudTramite { get; set; }
    public long IdTipoTramite { get; set; }
    public long IdRentista { get; set; }
    public long IdFuncionario { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public int IdEstadoTramite { get; set; }
    public bool Reasignado { get; set; }

    // Navigation properties
    public TipoTramiteDTO? TipoTramite { get; set; }
    public RentistaDTO? Rentista { get; set; }
}

/// <summary>
/// DTO temporal para detalles completos del trámite
/// </summary>
public class DetallesTramiteDTO
{
    public long IdSolicitudTramite { get; set; }
    public long IdTipoTramite { get; set; }
    public long IdRentista { get; set; }
    public long IdFuncionario { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public int IdEstadoTramite { get; set; }
    public bool Reasignado { get; set; }

    // Navigation properties
    public TipoTramiteDTO? TipoTramite { get; set; }
    public RentistaDTO? Rentista { get; set; }
}

/// <summary>
/// DTO temporal para archivos de trámite
/// </summary>
public class ArchivoTramiteDTO
{
    public long IdArchivo { get; set; }
    public long IdSolicitudTramite { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public int TamanioArchivo { get; set; } // en KB
    public string UrlArchivo { get; set; } = string.Empty;
    public string UrlDescarga { get; set; } = string.Empty;
}

/// <summary>
/// DTO temporal para resumen del dashboard
/// </summary>
public class ResumenDashboardFuncionarioDTO
{
    public int PendientesCount { get; set; }
    public int CompletadosCount { get; set; }
    public int UrgentesCount { get; set; }
    public int TotalProcesados { get; set; }
    public List<SolicitudTramiteDTO> RecentTramites { get; set; } = new();
}

/// <summary>
/// DTO temporal para tipos de trámites
/// </summary>
public class TipoTramiteDTO
{
    public long IdTipoTramite { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int DuracionDias { get; set; }
}

/// <summary>
/// DTO temporal para rentistas
/// </summary>
public class RentistaDTO
{
    public long IdRentista { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
}

// #endregion

using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.Reportes;
using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Back.Logica_De_Negocio.Services.Reportes
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _repository;

        public ReporteService(IReporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReporteDashboardDTO> GetDashboardDataAsync(int year)
        {
            return new ReporteDashboardDTO
            {
                TramitesPorMes = await _repository.GetTramitesPorMesAsync(year),
                RentistasPorEdad = await _repository.GetRentistasPorEdadAsync(),
                EstadosTramites = await _repository.GetEstadoTramitesAsync(),
                IncumplimientosTop = await _repository.GetIncumplimientosTopAsync()
            };
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.SolicitudTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.SolicitudTramiteSvc
{
    public class SolicitudTramiteService : ISolicitudTramiteService
    {
        private readonly ISolicitudTramiteRepository _repository;

        public SolicitudTramiteService(ISolicitudTramiteRepository repository)
        {
            _repository = repository;
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TipoTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.TipoTramiteSvc
{
    public class TipoTramiteService : ITipoTramiteService
    {
        private readonly ITipoTramiteRepository _repository;

        public TipoTramiteService(ITipoTramiteRepository repository)
        {
            _repository = repository;
        }
    }
}

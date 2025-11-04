using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.TipoArchivoRep;
using MiTramite_Domain.Entities;


namespace MiTramite_Back.Logica_De_Negocio.Services.TipoArchivoSvc
{
    public class TipoArchivoService : ITipoArchivoService
    {
        private readonly ITipoArchivoRepository _repository;

        public TipoArchivoService(ITipoArchivoRepository repository)
        {
            _repository = repository;
        }
    }
}

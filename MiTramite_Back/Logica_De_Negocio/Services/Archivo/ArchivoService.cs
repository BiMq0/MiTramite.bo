using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivoRep;
// using fully-qualified entity types to avoid collision with namespace names

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivoSvc
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _repository;

        public ArchivoService(IArchivoRepository repository)
        {
            _repository = repository;
        }
    }
}

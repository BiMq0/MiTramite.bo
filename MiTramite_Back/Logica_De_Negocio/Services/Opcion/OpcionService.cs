using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// using fully-qualified entity types to avoid collision with namespace names
using MiTramite_Back.Acceso_A_Datos.Repositories.OpcionRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.OpcionSvc
{
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _repository;

        public OpcionService(IOpcionRepository repository)
        {
            _repository = repository;
        }
    }
}

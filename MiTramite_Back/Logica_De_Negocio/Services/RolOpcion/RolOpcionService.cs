using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.RolOpcionRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolOpcionSvc
{
    public class RolOpcionService : IRolOpcionService
    {
        private readonly IRolOpcionRepository _repository;

        public RolOpcionService(IRolOpcionRepository repository)
        {
            _repository = repository;
        }
    }
}

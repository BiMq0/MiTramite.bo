using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Back.Acceso_A_Datos.Repositories.RolPermisoRep;

namespace MiTramite_Back.Logica_De_Negocio.Services.RolPermisoSvc
{
    public class RolPermisoService : IRolPermisoService
    {
        private readonly IRolPermisoRepository _repository;

        public RolPermisoService(IRolPermisoRepository repository)
        {
            _repository = repository;
        }
    }
}

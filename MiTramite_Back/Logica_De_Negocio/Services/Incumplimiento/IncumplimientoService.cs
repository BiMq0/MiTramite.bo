using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.IncumplimientoRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.IncumplimientoSvc
{
    public class IncumplimientoService : IIncumplimientoService
    {
        private readonly IIncumplimientoRepository _repository;

        public IncumplimientoService(IIncumplimientoRepository repository)
        {
            _repository = repository;
        }
    }
}

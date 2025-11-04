using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiTramite_Back.Acceso_A_Datos.Repositories.ArchivosRequeridosTramiteRep;
using MiTramite_Domain.Entities;

namespace MiTramite_Back.Logica_De_Negocio.Services.ArchivosRequeridosTramiteSvc
{
    public class ArchivosRequeridosTramiteService : IArchivosRequeridosTramiteService
    {
        private readonly IArchivosRequeridosTramiteRepository _repository;

        public ArchivosRequeridosTramiteService(IArchivosRequeridosTramiteRepository repository)
        {
            _repository = repository;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Back.AccessMaps;

namespace MiTramite_Back.Handlers
{
    public static class WebApplicationExtensions
    {
        public static void MapEndpoints(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                ArchivoMapper.Map(app);
                EstadoTramiteMapper.Map(app);
                FuncionarioMapper.Map(app);
                IncumplimientoMapper.Map(app);
                OpcionMapper.Map(app);
                PermisoMapper.Map(app);
                RentistaMapper.Map(app);
                RolMapper.Map(app);
                SolicitudTramiteMapper.Map(app);
                TipoArchivoMapper.Map(app);
                TipoTramiteMapper.Map(app);
            }
        }
    }
}
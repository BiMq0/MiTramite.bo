using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MiTramite_Back.AccessMaps;
using MiTramite_Back.Logica_De_Negocio.AccessMaps;

namespace MiTramite_Back.Handlers
{
    public static class WebApplicationExtensions
    {
        public static void MapEndpoints(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                // Autenticación
                FuncionarioMapper.Map(app);
                RentistaMapper.Map(app);

                // Archivos y Trámites
                ArchivoMapper.Map(app);
                TipoArchivoMapper.Map(app);
                TipoTramiteMapper.Map(app);
                SolicitusTramiteMapper.Map(app);
                IncumplimientoMapper.Map(app);
                ReporteMapper.Map(app);
            }
        }
    }
}
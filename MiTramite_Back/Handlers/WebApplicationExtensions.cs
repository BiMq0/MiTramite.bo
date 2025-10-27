using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiTramite_Back.Handlers
{
    public static class WebApplicationExtensions
    {
        public static void MapEndpoints(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                // Agrega más mapeadores según sea necesario
                // var nombreMapper = scope.ServiceProvider.GetRequiredService<NombreMapper>();
                // nombreMapper.Map(app);
            }
        }
    }
}
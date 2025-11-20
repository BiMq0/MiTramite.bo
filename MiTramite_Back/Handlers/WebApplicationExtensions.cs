using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MiTramite_Back.AccessMaps;

namespace MiTramite_Back.Handlers
{
    public static class WebApplicationExtensions
    {
        public static void MapEndpoints(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                FuncionarioMapper.Map(app);
                RentistaMapper.Map(app);
            }
        }
    }
}
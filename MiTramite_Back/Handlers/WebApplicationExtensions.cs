using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiTramite_Back.AccessMaps;
using MiTramite_Back.Middleware;
using MiTramite_Back.Middleware.Tokens;

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

        public static void AddMiddleware(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value?.ToLower();

                string[] allowedPaths = new string[]
                {
                    "/swagger",
                    "/login",
                    "/signup",
                    "/logout",
                    "/verify"
                };
                if (allowedPaths.Any(p => path!.Contains(p)))
                {
                    await next();
                    return;
                }

                var token = context.Request.Cookies["token"];

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token requerido");
                    return;
                }

                var tokenService = context.RequestServices.GetRequiredService<ITokenService>();
                await tokenService.ValidarToken(context, next, token);
            });
        }
    }
}
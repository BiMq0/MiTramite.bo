using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiTramite_Back.Middleware.Tokens
{
    public interface ITokenService
    {
        Task ValidarToken(HttpContext context, Func<Task> next, string token);
    }
}
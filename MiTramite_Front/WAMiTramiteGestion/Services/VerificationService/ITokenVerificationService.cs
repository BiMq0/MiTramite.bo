using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAMiTramiteGestion.Services.VerificationService
{
    public interface ITokenVerificationService
    {
        Task<bool> TokenExistsOrIsValidAsync();
    }
}
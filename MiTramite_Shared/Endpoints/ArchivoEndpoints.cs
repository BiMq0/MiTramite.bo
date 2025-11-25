using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiTramite_Shared.Endpoints
{
    public static class ArchivoEndpoints
    {
        public const string BASE = "archivo";
        public const string OBTENER_TODOS = "/obtener-todos";
        public const string OBTENER_DOCUMENTOS_POR_RENTISTA = "/obtener-documentos-por-rentista/{idRentista}";
        public const string SUBIR_DOCUMENTO = "/subir-documento/{idRentista}";
        public const string ELIMINAR_DOCUMENTO = "/eliminar-documento/{idDocumento}";
    }
}
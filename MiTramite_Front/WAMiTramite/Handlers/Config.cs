using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAMiTramite.Handlers
{
    public static class Config
    {
        public static string ApiUrl { get; set; } = "http://localhost:5080/";
        public static int Timeout { get; set; } = 30;


    }
}
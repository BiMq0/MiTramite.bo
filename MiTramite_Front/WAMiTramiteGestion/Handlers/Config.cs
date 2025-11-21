using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAMiTramiteGestion.Handlers
{
    public static class Config
    {
        //public static string ApiUrl { get; set; } = "https://localhost:5080/";
        public static string ApiUrl { get; set; } = "https://localhost:7204/";
        public static int Timeout { get; set; } = 30;


    }
}
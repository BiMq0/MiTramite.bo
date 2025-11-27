using System;

namespace MiTramite_Domain.Constants
{
    public enum TramiteEstados
    {
        Pendiente = 1,
        EnProceso = 2,
        Completado = 3,
        Rechazado = 4,
        Urgente = 5
    }

    public static class TramiteEstadosHex
    {
        // Provide hex color strings for UI mapping
        public const string Pendiente = "#F0AD4E"; // orange
        public const string EnProceso = "#5BC0DE"; // blue
        public const string Completado = "#5CB85C"; // green
        public const string Rechazado = "#D9534F"; // red
        public const string Urgente = "#8b1710ff"; // red

        public static string HexFor(TramiteEstados estado) => estado switch
        {
            TramiteEstados.Pendiente => Pendiente,
            TramiteEstados.EnProceso => EnProceso,
            TramiteEstados.Completado => Completado,
            TramiteEstados.Rechazado => Rechazado,
            _ => "#000000"
        };
    }
}

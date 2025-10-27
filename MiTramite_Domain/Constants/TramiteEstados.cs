using System;

namespace MiTramite_Domain.Constants
{
    public enum TramiteEstados
    {
        Pendiente = 1,
        EnProceso = 2,
        Completado = 3,
        Rechazado = 4,
        Cancelado = 5
    }

    public static class TramiteEstadosHex
    {
        // Provide hex color strings for UI mapping
        public const string Pendiente = "#F0AD4E"; // orange
        public const string EnProceso = "#5BC0DE"; // blue
        public const string Completado = "#5CB85C"; // green
        public const string Rechazado = "#D9534F"; // red
        public const string Cancelado = "#6C757D"; // gray

        public static string HexFor(TramiteEstados estado) => estado switch
        {
            TramiteEstados.Pendiente => Pendiente,
            TramiteEstados.EnProceso => EnProceso,
            TramiteEstados.Completado => Completado,
            TramiteEstados.Rechazado => Rechazado,
            TramiteEstados.Cancelado => Cancelado,
            _ => "#000000"
        };
    }
}

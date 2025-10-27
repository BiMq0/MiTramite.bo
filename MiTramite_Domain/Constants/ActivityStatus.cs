namespace MiTramite_Domain.Constants
{
    /// <summary>
    /// Estado de actividad para entidades que soportan borrado lógico (por ejemplo: Funcionario).
    /// </summary>
    public enum ActivityStatus
    {
        Inactivo = 0,
        Activo = 1
    }

    public static class ActivityStatusHex
    {
        // Colores típicos para UI
        public const string Activo = "#28A745"; // verde
        public const string Inactivo = "#6C757D"; // gris

        public static string HexFor(ActivityStatus s) => s switch
        {
            ActivityStatus.Activo => Activo,
            ActivityStatus.Inactivo => Inactivo,
            _ => "#000000"
        };
    }
}

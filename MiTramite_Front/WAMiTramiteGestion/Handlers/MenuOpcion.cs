namespace WAMiTramiteGestion.Handlers
{
    /// <summary>
    /// Representa una opción de menú en la navegación lateral
    /// </summary>
    public class MenuOpcion
    {
        /// <summary>
        /// Nombre visible de la opción en la interfaz
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Ruta de navegación
        /// </summary>
        public string Ruta { get; set; } = string.Empty;

        /// <summary>
        /// Ícono de Bootstrap Icons (ej: "bi-house", "bi-file-earmark-text")
        /// </summary>
        public string Icono { get; set; } = "bi-dot";

        /// <summary>
        /// Orden de presentación en el menú
        /// </summary>
        public int Orden { get; set; } = 0;
    }
}

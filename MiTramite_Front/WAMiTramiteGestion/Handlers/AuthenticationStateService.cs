using MiTramite_Shared.DTOs.FuncionarioDTOs;

namespace WAMiTramiteGestion.Handlers
{
    /// <summary>
    /// Servicio para gestionar el estado de autenticación y las opciones de menú dinámicas
    /// </summary>
    public class AuthenticationStateService
    {
        /// <summary>
        /// Evento que se dispara cuando el usuario inicia sesión
        /// </summary>
        public event Func<FuncionarioAccesosDTO, Task>? OnLoginAsync;

        /// <summary>
        /// Evento que se dispara cuando el usuario cierra sesión
        /// </summary>
        public event Action? OnLogout;

        /// <summary>
        /// DTO del funcionario autenticado
        /// </summary>
        public FuncionarioAccesosDTO? FuncionarioActual { get; private set; }

        /// <summary>
        /// Opciones de menú dinámicas según el rol y permisos
        /// </summary>
        public List<MenuOpcion> OpcionesMenuActuales { get; private set; } = new();

        /// <summary>
        /// Notificar que el usuario ha iniciado sesión
        /// </summary>
        /// <param name="funcionarioAccesos">Datos del funcionario autenticado</param>
        public async Task NotifyLoginAsync(FuncionarioAccesosDTO funcionarioAccesos)
        {
            FuncionarioActual = funcionarioAccesos;
            BuildMenuOpciones(funcionarioAccesos);

            if (OnLoginAsync != null)
            {
                await OnLoginAsync.Invoke(funcionarioAccesos);
            }
        }

        /// <summary>
        /// Notificar que el usuario ha cerrado sesión
        /// </summary>
        public void NotifyLogout()
        {
            FuncionarioActual = null;
            OpcionesMenuActuales.Clear();
            OnLogout?.Invoke();
        }

        /// <summary>
        /// Construir las opciones de menú basadas en el rol y opciones disponibles
        /// </summary>
        private void BuildMenuOpciones(FuncionarioAccesosDTO funcionarioAccesos)
        {
            OpcionesMenuActuales.Clear();

            if (funcionarioAccesos?.Opciones == null || funcionarioAccesos.Opciones.Count == 0)
            {
                return;
            }

            // Mapeo de opciones disponibles del servidor a opciones de menú
            var opcionesDisponibles = ObtenerOpcionesSegunRol(funcionarioAccesos.Rol);

            foreach (var opcion in funcionarioAccesos.Opciones)
            {
                var menuOpcion = opcionesDisponibles.FirstOrDefault(o => o.Label.ToLower() == opcion.ToLower());
                if (menuOpcion != null)
                {
                    OpcionesMenuActuales.Add(menuOpcion);
                }
            }

            // Ordenar las opciones
            OpcionesMenuActuales = OpcionesMenuActuales.OrderBy(o => o.Orden).ToList();
        }

        /// <summary>
        /// Obtiene las opciones disponibles según el rol
        /// </summary>
        private List<MenuOpcion> ObtenerOpcionesSegunRol(string? rol)
        {
            // Si es gerente, mostrar opciones de gerente
            if (rol?.ToLower() == "gerente")
            {
                return new List<MenuOpcion>
                {
                    new MenuOpcion
                    {
                        Label = "Inicio",
                        Ruta = "/gerente/inicio",
                        Icono = "bi-house",
                        Orden = 1
                    },
                    new MenuOpcion
                    {
                        Label = "Trámites Generales",
                        Ruta = "/gerente/tramites-generales",
                        Icono = "bi-file-earmark-check",
                        Orden = 2
                    },
                    new MenuOpcion
                    {
                        Label = "Control",
                        Ruta = "/gerente/control",
                        Icono = "bi-people",
                        Orden = 3
                    },
                    new MenuOpcion
                    {
                        Label = "Reportes",
                        Ruta = "/gerente/reportes",
                        Icono = "bi-graph-up",
                        Orden = 4
                    }
                };
            }

            // Por defecto, opciones de funcionario
            return new List<MenuOpcion>
            {
                new MenuOpcion
                {
                    Label = "Inicio",
                    Ruta = "/funcionario/inicio",
                    Icono = "bi-house",
                    Orden = 1
                },
                new MenuOpcion
                {
                    Label = "Trámites",
                    Ruta = "/funcionario/tramites",
                    Icono = "bi-file-earmark-text",
                    Orden = 2
                },
                new MenuOpcion
                {
                    Label = "Trámites Pendientes",
                    Ruta = "/funcionario/tramites-pendientes",
                    Icono = "bi-clock-history",
                    Orden = 3
                },
                new MenuOpcion
                {
                    Label = "Reportes",
                    Ruta = "/funcionario/reportes",
                    Icono = "bi-graph-up",
                    Orden = 4
                }
            };
        }
    }
}

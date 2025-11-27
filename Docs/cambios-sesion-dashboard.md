# Cambios realizados

## 1. Persistencia de sesión para funcionarios

- **Program.cs**: se registró `ProtectedLocalStorage` y quedó disponible para los servicios scoped, manteniendo la configuración híbrida de componentes.
- **Services/Funcionario/IFuncionarioService.cs**: la interfaz ahora publica `ObtenerFuncionarioActualAsync()` y `CerrarSesionAsync()` para exponer la nueva lógica de sesión.
- **Services/Funcionario/FuncionarioService.cs**:
  - Se inyectan `ProtectedLocalStorage` y `LoginStateService`.
  - `IniciarSesion` persiste el funcionario autenticado, comparte el estado con `LoginStateService` y maneja errores de almacenamiento local.
  - `ObtenerFuncionarioActualAsync` restaura el funcionario desde almacenamiento seguro cuando la instancia en memoria es nula.
  - `CerrarSesionAsync` limpia la caché persistida y emite `NotifyLogout`.
  - Se añadieron helpers privados para guardar/cargar y sincronizar el estado con el servicio global de login.

## 2. Componentes y páginas que consumen la sesión persistida

- **Components/Pages/Funcionario/Inicio.razor**: usa `ObtenerFuncionarioActualAsync`, agrega `NavigationManager` para redirigir al login si no hay sesión y establece `@rendermode InteractiveServer` para el dashboard.
- **Components/Pages/Funcionario/TramitesPendientes.razor** y **HIstorialTramites.razor**: antes asumían que `FuncionarioActual` existía; ahora cargan la sesión desde el servicio y manejan el caso sin autenticación.
- **Components/Pages/Login.razor**: al inicializar valida el token y consulta el funcionario persistido para redirigir automáticamente sin requerir una nueva autenticación manual.

## 3. Layouts y encabezado

- **Components/Layout/MainLayout.razor**: cambió a `OnInitializedAsync`, inyecta `IFuncionarioService` y restablece el rol desde la sesión persistida cuando la app se recarga.
- **Components/Layout/Header.razor**: ahora implementa `IDisposable`, se suscribe/desuscribe de `LoginStateService` y carga los datos del funcionario desde el almacenamiento seguro al iniciarse.
- **Components/Layout/NavMenu.razor**: el botón "Cerrar Sesión" llama a `CerrarSesionAsync()` del servicio y luego fuerza navegación a `/login` para limpiar cualquier rastro de sesión.

## 4. Vistas adicionales ajustadas

- **Components/Pages/Funcionario/HIstorialTramites.razor** se reescribió para corregir rupturas anteriores y asegurar que utilice la misma lógica de sesión persistida.

## 5. Estilos y render modes

- Páginas clave (ej. `Inicio.razor`) declararon explícitamente `@rendermode InteractiveServer` para garantizar coherencia con el comportamiento interactivo tras las recargas.

---

Estos cambios garantizan que el funcionario autenticado se mantenga en almacenamiento local seguro, reduciendo las pérdidas de sesión tras recargas o navegación entre páginas, y que todos los componentes consulten ese estado centralizado antes de operar.

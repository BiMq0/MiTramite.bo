# MiTramite_Front - Aplicaciones Frontend

## Propósito

Este directorio contiene las **dos aplicaciones frontend** del sistema MiTramite.bo, desarrolladas en Blazor Server para atender diferentes tipos de usuarios con interfaces y funcionalidades específicas.

## Aplicaciones del Ecosistema

### 🏛️ **WAMiTramiteGestion** - Panel de Funcionarios

**Usuarios objetivo**: Funcionarios públicos y administradores del sistema

**Funcionalidades principales**:

- **Gestión de trámites**: Revisar, aprobar o rechazar solicitudes ciudadanas
- **Panel de administración**: Estadísticas, reportes y métricas del sistema
- **Gestión de usuarios**: Administrar perfiles de ciudadanos y funcionarios
- **Procesamiento de documentos**: Validar y gestionar documentos adjuntos
- **Comunicación con solicitantes**: Enviar notificaciones y solicitar información adicional
- **Auditoria**: Seguimiento de acciones y cambios realizados
- **Configuración del sistema**: Parametrización de tipos de trámites y requisitos

**Características técnicas**:

- Autenticación robusta con roles diferenciados
- Interface administrativa optimizada para productividad
- Dashboards con métricas en tiempo real
- Herramientas de búsqueda y filtrado avanzadas
- Integración completa con SignalR para notificaciones

---

### 👥 **WAMiTramite** - Portal Ciudadano

**Usuarios objetivo**: Rentistas, ciudadanos y público en general

**Funcionalidades principales**:

- **Solicitud de trámites**: Formularios intuitivos para Renta Dignidad y Afiliación
- **Seguimiento de solicitudes**: Consulta del estado de trámites en proceso
- **Carga de documentos**: Interface simple para adjuntar documentación requerida
- **Perfil personal**: Gestión de datos personales y contacto
- **Notificaciones**: Alertas sobre cambios de estado y requerimientos
- **Historial de trámites**: Consulta de solicitudes anteriores
- **Ayuda y guías**: Información sobre requisitos y procesos

**Características técnicas**:

- Interface responsive y accesible
- Proceso de registro/login simplificado
- Validaciones en tiempo real de formularios
- Carga de archivos con preview y validación
- Notificaciones push en tiempo real

## Arquitectura Frontend Compartida

### **Tecnologías Base**

- **Blazor Server**: Framework para aplicaciones web interactivas con C#
- **Bootstrap 5**: Framework CSS para diseño responsive
- **SignalR Client**: Comunicación en tiempo real con el backend
- **Blazored Components**: Librerías adicionales para Blazor

### **Estructura Común**

```
WAMiTramite[Gestion]/
├── Components/              # Componentes Blazor reutilizables
│   ├── Layout/             # Layouts principales
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   └── Shared/            # Componentes compartidos
├── Pages/                  # Páginas principales
│   ├── Home.razor
│   ├── Tramites/
│   └── Usuario/
├── Services/              # Servicios de frontend
│   ├── ApiService.cs
│   ├── AuthService.cs
│   └── SignalRService.cs
├── wwwroot/               # Archivos estáticos
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs             # Configuración de la aplicación
└── appsettings.json      # Configuraciones
```

## Diferencias Clave entre Aplicaciones

| Característica        | WAMiTramite (Ciudadanos) | WAMiTramiteGestion (Funcionarios) |
| --------------------- | ------------------------ | --------------------------------- |
| **Público objetivo**  | Ciudadanos, rentistas    | Funcionarios, administradores     |
| **Función principal** | Solicitar trámites       | Gestionar y aprobar trámites      |
| **Nivel de acceso**   | Limitado a datos propios | Acceso completo según rol         |
| **Interface**         | Simplificada, intuitiva  | Completa, funcional               |
| **Autenticación**     | Básica (CI/email)        | Robusta (múltiples roles)         |
| **Notificaciones**    | Estado de sus trámites   | Nuevas solicitudes y alertas      |

## Variables de Entorno

### WAMiTramite

```
BACKEND_API_URL=https://api.mitramite.bo
SIGNALR_HUB_URL=https://api.mitramite.bo/notifications
ENVIRONMENT=Production
```

### WAMiTramiteGestion

```
BACKEND_API_URL=https://api.mitramite.bo
SIGNALR_HUB_URL=https://api.mitramite.bo/admin-notifications
ADMIN_FEATURES_ENABLED=true
ENVIRONMENT=Production
```

## Comandos de Ejecución

### Desarrollo

```bash
# Portal ciudadano
dotnet run --project WAMiTramite/WAMiTramite.csproj

# Panel funcionarios
dotnet run --project WAMiTramiteGestion/WAMiTramiteGestion.csproj
```

### Producción

```bash
dotnet publish -c Release
```

## Puertos de Desarrollo

- **WAMiTramite**: `https://localhost:7002`
- **WAMiTramiteGestion**: `https://localhost:7003`

## Integración con Backend

Ambas aplicaciones consumen:

- **MiTramite_Back** APIs para lógica de negocio
- **MiTramite_Shared** DTOs y contratos compartidos
- **SignalR** para notificaciones en tiempo real
- **Supabase** para autenticación (a través del backend)

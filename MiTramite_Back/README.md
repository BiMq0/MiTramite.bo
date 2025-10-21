# MiTramite_Back - Backend API

## Propósito

Este proyecto constituye el **backend principal** del sistema MiTramite.bo, implementando toda la lógica de negocio y proporcionando acceso seguro a la base de datos a través de APIs REST.

## Funcionalidad dentro del Ecosistema

El backend actúa como la **capa de servicios centralizada** que:

- **Procesa la lógica de negocio**: Maneja las reglas y validaciones para los trámites de Afiliación a Trabajador Dependiente y Renta Dignidad
- **Gestiona acceso a datos**: Conecta con Supabase (PostgreSQL) para operaciones CRUD seguras
- **Expone APIs REST**: Provee endpoints para que los frontends (WAMiTramite y WAMiTramiteGestion) consuman los servicios
- **Maneja autenticación y autorización**: Controla el acceso diferenciado entre ciudadanos y funcionarios
- **Procesa documentos**: Gestiona la carga, validación y almacenamiento de documentos adjuntos
- **Notificaciones en tiempo real**: Integra SignalR para comunicar cambios de estado de trámites

## Tecnologías Principales

- **.NET 9 Minimal APIs**: Framework ligero para crear endpoints REST eficientes
- **Entity Framework Core**: ORM para acceso a datos con Supabase/PostgreSQL
- **SignalR**: Comunicación bidireccional en tiempo real
- **JWT Authentication**: Tokens seguros para autenticación
- **Swagger/OpenAPI**: Documentación automática de APIs

## Estructura del Proyecto

```
MiTramite_Back/
├── Mappers/         # Acceso a servicios de lógica de negocio
├── Services/           # Lógica de negocio
├── Repositories/       # Acceso a datos
├── Program.cs         # Configuración de la aplicación
├── Handlers/           # Automatización de configuraciones builder
└── appsettings.json   # Configuraciones
```

## Variables de Entorno Requeridas

- `SUPABASE_CONNECTION_STRING`: Cadena de conexión a PostgreSQL
- `ASPNETCORE_ENVIRONMENT`: Entorno de ejecución

## Comando de Ejecución

```bash
dotnet run
```

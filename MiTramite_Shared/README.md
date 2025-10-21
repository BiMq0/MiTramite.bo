# MiTramite_Shared - Capa de Datos Compartidos

## Propósito

Este proyecto contiene **elementos compartidos** entre el frontend y backend del sistema MiTramite.bo, facilitando la comunicación consistente y tipada entre ambas capas de la aplicación.

## Funcionalidad dentro del Ecosistema

La capa compartida actúa como un **puente de comunicación** que:

- **Estandariza DTOs**: Define objetos de transferencia de datos consistentes entre frontend y backend
- **Centraliza contratos de API**: Especifica endpoints, rutas y parámetros de las APIs
- **Comparte enumeraciones**: Tipos comunes usados tanto en cliente como servidor
- **Define modelos de comunicación**: Estructuras para SignalR y comunicación en tiempo real
- **Mantiene consistencia**: Asegura que ambos extremos manejen los mismos tipos de datos

## Estructura del Proyecto

```
MiTramite_Shared/
├── DTOs/                    # Data Transfer Objects
│   ├── TramiteDto.cs
│   ├── UsuarioDto.cs
│   ├── DocumentoDto.cs
│   └── EstadoTramiteDto.cs
├── Endpoints/              # Definición de rutas de API
│   ├── TramiteEndpoints.cs
│   ├── UsuarioEndpoints.cs
│   └── DocumentoEndpoints.cs
├── Enums/                  # Enumeraciones compartidas
│   ├── TipoTramite.cs
│   ├── EstadoTramite.cs
│   └── TipoDocumento.cs
├── Models/                 # Modelos específicos de comunicación
│   ├── LoginRequest.cs
│   ├── ApiResponse.cs
│   └── SignalRMessage.cs
├── Constants/              # Constantes del sistema
│   ├── RoleConstants.cs
│   ├── StatusConstants.cs
│   └── EndpointConstants.cs
└── Validators/            # Validaciones compartidas
    ├── TramiteValidator.cs
    └── DocumentoValidator.cs
```

## Componentes Principales

### **DTOs (Data Transfer Objects)**

Objetos ligeros para transferir datos entre capas:

```csharp
public class TramiteDto
{
    public int Id { get; set; }
    public string NumeroTramite { get; set; }
    public TipoTramite Tipo { get; set; }
    public EstadoTramite Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    // ...
}
```

### **Endpoints Constants**

Rutas centralizadas para mantener consistencia:

```csharp
public static class TramiteEndpoints
{
    public const string Base = "/api/tramites";
    public const string GetById = Base + "/{id}";
    public const string Create = Base;
    public const string UpdateEstado = Base + "/{id}/estado";
}
```

### **Enumeraciones Compartidas**

Tipos comunes usados en frontend y backend:

```csharp
public enum TipoTramite
{
    RentaDignidad = 1,
    AfiliacionTrabajadorDependiente = 2
}

public enum EstadoTramite
{
    Pendiente = 1,
    EnRevision = 2,
    Aprobado = 3,
    Rechazado = 4
}
```

### **Modelos de Comunicación**

Estructuras específicas para APIs y SignalR:

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}
```

## Ventajas de la Capa Compartida

### **Consistencia de Tipos**

- Mismas definiciones en cliente y servidor
- Reduce errores de serialización/deserialización
- IntelliSense compartido entre proyectos

### **Mantenimiento Centralizado**

- Un solo lugar para modificar contratos
- Cambios automáticamente reflejados en ambos extremos
- Versionado coordinado de APIs

### **Validaciones Uniformes**

- Mismas reglas de validación en frontend y backend
- Experiencia de usuario consistente
- Seguridad reforzada

## Referencias del Proyecto

**Referenciado por:**

- `MiTramite_Back` - Usa DTOs y endpoints
- `MiTramite_Front/WAMiTramite` - Consume DTOs desde el cliente
- `MiTramite_Front/WAMiTramiteGestion` - Consume DTOs desde el panel de gestión

**Referencias a:**

- `MiTramite_Domain` - Para mapear entidades a DTOs

## Comandos de Compilación

```bash
dotnet build MiTramite_Shared.csproj
dotnet pack # Para crear paquete NuGet (opcional)
```

## Consideraciones de Versionado

- Mantener compatibilidad hacia atrás en DTOs
- Usar versionado semántico para cambios
- Documentar breaking changes en DTOs
- Considerar DTOs separados por versión de API si es necesario

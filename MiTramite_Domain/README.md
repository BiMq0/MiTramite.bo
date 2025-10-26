# MiTramite_Domain - Capa de Dominio

## Propósito

Este proyecto implementa la **capa de dominio** del sistema MiTramite.bo, siguiendo los principios de **Clean Architecture**. Contiene las entidades de negocio fundamentales, reglas de dominio y contratos que definen el núcleo del sistema.

## Funcionalidad dentro del Ecosistema

La capa de dominio representa el **corazón del sistema** y:

- **Define entidades de negocio**: Modelos que representan conceptos del mundo real (Trámite, Usuario, Documento, etc.)
- **Establece reglas de dominio**: Lógica de negocio pura e invariantes del sistema
- **Mantiene independencia**: No depende de frameworks externos, bases de datos o UI
- **Garantiza consistencia**: Asegura que las reglas de negocio se respeten en todo el sistema

## Principios de Clean Architecture Aplicados

### 1. **Independencia de Frameworks**

Las entidades no dependen de Entity Framework, ASP.NET u otros frameworks externos.

### 2. **Independencia de UI**

El dominio no conoce si será consumido por Blazor, API REST o cualquier otra interfaz.

### 3. **Independencia de Base de Datos**

Las entidades no dependen de si los datos se almacenan en PostgreSQL, MySQL o cualquier otro motor.

### 4. **Reglas de Negocio Centralizadas**

Toda la lógica de negocio crítica reside en esta capa.

## Estructura del Proyecto

```
MiTramite_Domain/
└── Entities/              # Entidades de dominio
   ├── Tramite.cs
   ├── Usuario.cs
   ├── Documento.cs
   ├── Rol.cs
   └── Permiso.cs
└── Constants/              # Entidades de dominio
    ├── TiposTramite.cs
    └── EstadosTramite.cs

```

## Entidades Principales

### **Tramite**

- Representa una solicitud de trámite (Renta Dignidad, Afiliación)
- Maneja estados, fechas, validaciones de negocio

### **Usuario**

- Ciudadano solicitante o funcionario del sistema
- Contiene datos personales, roles y permisos

### **Documento**

- Archivos adjuntos requeridos para cada trámite
- Validaciones de formato, tamaño y obligatoriedad

### **Rol**

- Papel del usuario dentro del sistema (Ciudadano, Funcionario)
- Define permisos y accesos sin depender directamente de permisos predefinidos

### **Permiso**

- Acciones específicas que un rol puede realizar
- Facil escalabilidad y asignacion a nuevos roles

## Constantes

### **TiposTramite**

- Define los tipos de trámites disponibles en el sistema

### **EstadosTramite**

- Enumera los posibles estados de un trámite (Pendiente, En Revisión, Aprobado, Rechazado)
- Define colores por estado para UI

## Reglas de Negocio Críticas

- **Validación de identidad**: CI debe ser válido y único
- **Requisitos por trámite**: Cada tipo de trámite tiene documentos obligatorios específicos
- **Flujo de estados**: Los trámites siguen un flujo específico (Pendiente → En Revisión → Aprobado/Rechazado)
- **Permisos diferenciados**: Ciudadanos y funcionarios tienen accesos distintos

## Patrones Implementados

- **Entity Pattern**: Entidades con identidad única

## Comandos de Compilación

```bash
dotnet build MiTramite_Domain.csproj
dotnet test # (cuando se agreguen tests unitarios)
```

## Referencias

Esta capa es referenciada por:

- `MiTramite_Back` (implementa interfaces del dominio)
- `MiTramite_Shared` (comparte DTOs basados en entidades)

No referencia ningún otro proyecto del sistema, manteniendo su independencia.

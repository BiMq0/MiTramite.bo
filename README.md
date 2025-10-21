# MiTramite.bo - Sistema de Digitalización de Trámites Públicos

MiTramite es un **sistema integral** para digitalizar trámites públicos de la población boliviana. Está diseñado para simplificar y acelerar procesos administrativos, reducir papeleo y proporcionar trazabilidad completa a las solicitudes ciudadanas.

## 🎯 Trámites Soportados (Primera Versión)

- **Afiliación a Trabajador Dependiente**
- **Acceso a Renta Dignidad**

El objetivo es ofrecer una plataforma web responsiva donde ciudadanos y funcionarios puedan gestionar solicitudes, adjuntar documentos y recibir notificaciones en tiempo real sobre el estado de sus trámites.

## 🏗️ Arquitectura del Sistema

El proyecto sigue principios de **Clean Architecture** con separación clara de responsabilidades:

```
MiTramite.bo/
├── 🌐 MiTramite_Front/          # Aplicaciones Frontend (Blazor Server)
│   ├── WAMiTramite/             # Portal para ciudadanos
│   └── WAMiTramiteGestion/      # Panel para funcionarios
├── 🔧 MiTramite_Back/           # Backend API (.NET 9 Minimal APIs)
├── 🏛️ MiTramite_Domain/        # Capa de Dominio (Clean Architecture)
└── 🔄 MiTramite_Shared/         # DTOs y contratos compartidos
```

### **MiTramite_Front** - Interfaces de Usuario

- **WAMiTramite**: Portal intuitivo para ciudadanos y rentistas
- **WAMiTramiteGestion**: Panel administrativo para funcionarios
- **Tecnología**: Blazor Server, Bootstrap 5, SignalR Client

### **MiTramite_Back** - API Backend

- **Propósito**: Lógica de negocio y acceso seguro a datos
- **Funciones**: APIs REST, autenticación, procesamiento de documentos
- **Tecnología**: .NET 9 Minimal APIs, Entity Framework, SignalR

### **MiTramite_Domain** - Núcleo del Sistema

- **Propósito**: Entidades de negocio y reglas de dominio
- **Implementa**: Principios de Clean Architecture
- **Contiene**: Entidades, interfaces, validaciones de negocio

### **MiTramite_Shared** - Comunicación Entre Capas

- **Propósito**: DTOs, endpoints y contratos compartidos
- **Facilita**: Comunicación tipada entre frontend y backend
- **Incluye**: Modelos de transferencia, constantes, validaciones

## 🛠️ Stack Tecnológico

### **Frontend**

- **Blazor Server**: Framework web interactivo con C#
- **Bootstrap 5**: Diseño responsive y componentes UI
- **SignalR Client**: Comunicación en tiempo real

### **Backend**

- **.NET 9 Minimal APIs**: APIs ligeras y eficientes
- **Entity Framework Core**: ORM para acceso a datos
- **JWT Authentication**: Autenticación segura
- **SignalR**: Notificaciones bidireccionales en tiempo real

### **Base de Datos y Servicios**

- **Supabase**: PostgreSQL + autenticación + tiempo real
- **Render**: Plataforma de hosting en la nube
- **Docker**: Contenedorización para despliegues

## 🚀 Flujo de Trabajo del Sistema

### **Para Ciudadanos** (WAMiTramite)

1. **Registro/Login** → Autenticación con CI o email
2. **Solicitud** → Llenar formularios de trámite requerido
3. **Documentos** → Cargar archivos adjuntos necesarios
4. **Seguimiento** → Consultar estado en tiempo real
5. **Notificaciones** → Recibir alertas de cambios de estado

### **Para Funcionarios** (WAMiTramiteGestion)

1. **Dashboard** → Vista general de solicitudes pendientes
2. **Revisión** → Evaluar documentos y requisitos
3. **Procesamiento** → Aprobar, rechazar o solicitar información
4. **Comunicación** → Notificar cambios a solicitantes
5. **Reportes** → Generar estadísticas y métricas

## 🔧 Instalación y Desarrollo

### **Prerrequisitos**

- .NET 9 SDK
- Visual Studio 2022 / VS Code
- PostgreSQL (o cuenta en Supabase)

### **Configuración Local**

```bash
# Clonar repositorio
git clone https://github.com/usuario/MiTramite.bo.git
cd MiTramite.bo

# Restaurar dependencias
dotnet restore

# Configurar variables de entorno
cp appsettings.example.json appsettings.Development.json

# Ejecutar migraciones
dotnet ef database update --project MiTramite_Back

# Ejecutar aplicaciones
dotnet run --project MiTramite_Back           # Backend API
dotnet run --project MiTramite_Front/WAMiTramite  # Portal ciudadano
dotnet run --project MiTramite_Front/WAMiTramiteGestion  # Panel funcionarios
```

### **Puertos de Desarrollo**

- **Backend API**: `https://localhost:7001`
- **Portal Ciudadano**: `https://localhost:7002`
- **Panel Funcionarios**: `https://localhost:7003`

## 🐳 Docker y Despliegue en Render

### **Contenedorización**

```bash
# Construir imagen del backend
docker build -t mitramite-back ./MiTramite_Back

# Construir imagen del frontend
docker build -t mitramite-front ./MiTramite_Front
```

### **Despliegue en Render**

1. **Configurar variables de entorno** en Render Dashboard
2. **Conectar repositorio** para despliegue automático
3. **Configurar comando de construcción** y puerto de exposición
4. **Variables críticas**: `SUPABASE_CONNECTION_STRING`, `JWT_SECRET`, `SIGNALR_SECRET`

> **⚠️ Importante**: Mantener variables sensibles en las settings de Render, nunca en el repositorio.

## 🛠️ Herramientas de Desarrollo

- **Visual Studio 2022**: Recomendado para desarrollo completo .NET y Blazor
- **Visual Studio Code**: Para edición rápida y tareas DevOps
- **Postman/Thunder Client**: Para pruebas de APIs
- **pgAdmin/DBeaver**: Para gestión de base de datos

## 📋 Funcionalidades Clave

### **Seguridad**

- Autenticación JWT con roles diferenciados
- Validación de documentos de identidad
- Cifrado de datos sensibles
- Auditoría de acciones críticas

### **Tiempo Real**

- Notificaciones push con SignalR
- Actualizaciones de estado instantáneas
- Chat/mensajería entre funcionarios y ciudadanos

### **Gestión Documental**

- Carga de múltiples formatos de archivo
- Validación automática de documentos
- Almacenamiento seguro en la nube
- Historial de versiones

### **Reportes y Métricas**

- Dashboard con estadísticas en tiempo real
- Reportes de productividad por funcionario
- Métricas de satisfacción ciudadana
- Análisis de tiempos de procesamiento

## 📚 Documentación Adicional

Cada proyecto contiene su propio `README.md` con información específica:

- [`MiTramite_Back/README.md`](./MiTramite_Back/README.md) - Documentación del backend
- [`MiTramite_Domain/README.md`](./MiTramite_Domain/README.md) - Capa de dominio y Clean Architecture
- [`MiTramite_Shared/README.md`](./MiTramite_Shared/README.md) - DTOs y contratos compartidos
- [`MiTramite_Front/README.md`](./MiTramite_Front/README.md) - Aplicaciones frontend

## 🤝 Contribución

1. Fork del proyecto
2. Crear rama feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit cambios (`git commit -am 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Crear Pull Request

---

**MiTramite.bo** - Digitalizando Bolivia, un trámite a la vez 🇧🇴

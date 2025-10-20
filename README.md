# MiTramite

MiTramite es un sistema para digitalizar trámites públicos de la población. Está pensado para simplificar y acelerar procesos administrativos, reducir papeleo y dar trazabilidad a las solicitudes. En esta primera versión el proyecto soporta, de forma destacada:

- Afiliación a trabajador dependiente
- Acceso a Renta Dignidad

El objetivo es ofrecer una plataforma web responsiva donde los ciudadanos y los funcionarios puedan gestionar solicitudes, adjuntar documentos y recibir notificaciones en tiempo real sobre el estado de sus trámites.

## Tecnologías

El proyecto combina tecnologías modernas en el front-end, back-end, comunicaciones en tiempo real y persistencia de datos. Breve descripción de cada una:

- Blazor Pages: framework de interfaz web que permite construir UI interactivas con C# y componentes reutilizables. Ideal para aplicaciones SPA (Single Page Application) usando .NET en el cliente/servidor.
- .NET Core Minimal APIs: APIs ligeras y rápidas para exponer los endpoints del servidor con una superficie de código reducida; facilitan crear microservicios y puntos de integración para el cliente.
- Render (hosting): plataforma de hosting en la nube que permite desplegar aplicaciones mediante contenedores o repositorios; se usa aquí como destino de despliegue para el backend y front-end compilado.
- Supabase: proveedor de base de datos y servicios (Postgres + autenticación + realtime) usado como capa de persistencia. Ofrece APIs y utilidades listas para integrarse con aplicaciones modernas.
- SignalR: biblioteca para comunicación en tiempo real entre servidor y clientes. Se utiliza para notificar cambios de estado de trámites, actualizaciones y mensajes en vivo.

## Docker y despliegue en Render

El repositorio incluye un `Dockerfile` (o puede añadirse uno) para crear la imagen del proyecto y desplegarla en Render. El flujo típico es construir la imagen del backend y frontend, empujarla a un y permitir que Render la construya desde el repositorio, y configurar las variables de entorno necesarias (cadenas de conexión, claves de Supabase, secretos de SignalR, etc.).

Consejos rápidos para despliegue:

- Mantener las variables sensibles en las settings de Render, no en el repositorio.
- Exponer el puerto configurado por Render en el contenedor y usar un comando de inicio que lance la API y/o la app Blazor según corresponda.

## Herramientas de desarrollo

- Visual Studio 2022 — recomendado para desarrollo, depuración y trabajo con proyectos .NET y Blazor.
- Visual Studio Code — ligero y práctico para edición rápida, scripts y tareas devops.

## Notas finales

Este README cubre los puntos esenciales para entender el propósito del proyecto y las tecnologías principales utilizadas.

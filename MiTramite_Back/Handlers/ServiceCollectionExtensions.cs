using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//using MiTramite_Back.Mappers;

namespace MiTramite_Back.Handlers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
        {
            // Todos los repositorios deben terminar con "Repository" para que se registren automáticamente
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            var interfaces = types.Where(t => t.IsInterface && t.Name.EndsWith("Repository"));

            foreach (var itf in interfaces)
            {
                var implementation = types.FirstOrDefault(t => itf.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
                if (implementation != null)
                {
                    services.AddScoped(itf, implementation);
                }
            }
            return services;
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            // Todos los servicios deben terminar con "Service" para que se registren automáticamente
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var interfaces = types.Where(t => t.IsInterface && t.Name.EndsWith("Service"));
            foreach (var itf in interfaces)
            {
                var implementation = types.FirstOrDefault(t => itf.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
                if (implementation != null)
                {
                    services.AddScoped(itf, implementation);
                }
            }
            return services;
        }

        public static IServiceCollection AddScopedMappers(this IServiceCollection services)
        {

            // Agrega más mapeadores según sea necesario
            // services.AddScoped<NombreMapper>();
            return services;
        }
    }
}
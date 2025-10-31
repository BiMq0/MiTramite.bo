using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAMiTramite.Handlers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            var types = assembly.GetTypes().Where(t => t.Name.EndsWith("Service"));
            var interfaces = types.Where(t => t.IsInterface).ToList();
            var implementations = types.Where(t => t.IsClass).ToList();

            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(t => @interface.IsAssignableFrom(t));
                if (implementation != null)
                {
                    services.AddScoped(@interface, implementation);
                }
            }

            return services;
        }
    }
}
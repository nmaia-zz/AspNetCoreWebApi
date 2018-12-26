using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Contracts;
using Project.Repository.Repositories;

namespace Project.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanetRepository, PlanetRepository>();
        }
    }
}

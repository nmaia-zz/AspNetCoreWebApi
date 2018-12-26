using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Contracts.Repositories;
using Project.Repository.Repositories;
using Project.RestfulConnector.Concrete;
using Project.RestfulConnector.Contracts;


namespace Project.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanetRepository, PlanetRepository>();
            services.AddScoped<ISwApiConnector, SwApiConnector>();
        }
    }
}

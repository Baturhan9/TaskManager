using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Repositories;
using TaskManager.Services;

namespace TaskManager.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddModelsServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
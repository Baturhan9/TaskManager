using TaskManager.Consts;
using TaskManager.Web.Clients;
using TaskManager.Web.Interfaces;

namespace TaskManager.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddTaskManagerClient(this IServiceCollection services, IConfiguration configuration)
    {
        string url = configuration.GetValue<string>(SystemEnvironments.TASK_MANAGER_URL) ?? string.Empty;
        if (url == string.Empty)
            throw new Exception($"{SystemEnvironments.TASK_MANAGER_URL} must not be empty");

        services.AddHttpClient<ITaskManagerClient,TaskManagerClient>(client => client.BaseAddress = new Uri(url));
        services.AddHttpContextAccessor();
        return services;
    }
}
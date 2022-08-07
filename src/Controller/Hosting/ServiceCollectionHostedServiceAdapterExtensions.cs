using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kubernetes.Controller.Hosting;

/// <summary>
/// Class ServiceCollectionHostedServiceAdapterExtensions.
/// </summary>
public static class ServiceCollectionHostedServiceAdapterExtensions
{
    /// <summary>
    /// Registers the hosted service.
    /// </summary>
    /// <typeparam name="TService">The type of the t service.</typeparam>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection RegisterHostedService<TService>(this IServiceCollection services)
        where TService : IHostedService
    {
        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(HostedServiceAdapter<TService>)))
        {
            services = services.AddHostedService<HostedServiceAdapter<TService>>();
        }

        return services;
    }
}
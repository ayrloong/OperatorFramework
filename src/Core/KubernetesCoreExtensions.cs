using k8s;
using Kubernetes.Core;
using Kubernetes.Core.ResourceKinds;
using Kubernetes.Core.Resources;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class KubernetesCoreExtensions.
/// </summary>
public static class KubernetesCoreExtensions
{
    /// <summary>
    /// Adds the kubernetes.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddKubernetesCore(this IServiceCollection services)
    {
        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IKubernetes)))
        {
            services = services.Configure<KubernetesClientOptions>(options =>
            {
                if (options.Configuration == null)
                {
                    options.Configuration = KubernetesClientConfiguration.BuildDefaultConfig();
                }
            });

            services = services.AddSingleton<IKubernetes>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KubernetesClientOptions>>().Value;

                return new k8s.Kubernetes(options.Configuration);
            });
        }

        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResourceSerializers)))
        {
            services = services.AddTransient<IResourceSerializers, ResourceSerializers>();
        }

        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResourcePatcher)))
        {
            services = services.AddTransient<IResourcePatcher, ResourcePatcher>();
        }

        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResourceKindManager)))
        {
            services = services.AddSingleton<IResourceKindManager, ResourceKindManager>();
        }

        return services;
    }
}
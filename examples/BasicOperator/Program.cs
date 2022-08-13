// See https://aka.ms/new-console-template for more information
using BasicOperator.Generators;
using BasicOperator.Models;
using k8s.Models;
using Kubernetes.Core.ResourceKinds;
using Kubernetes.Operator;
using Kubernetes.ResourceKinds.OpenApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    public static async Task<int> Main(string[] args)
    {
        
        var hostBuilder = new HostBuilder();

        hostBuilder.ConfigureHostConfiguration(hostConfiguration =>
        {
            hostConfiguration.AddCommandLine(args);
        });

        hostBuilder.ConfigureServices(services =>
        {
            services.AddTransient<IResourceKindProvider, OpenApiResourceKindProvider>();

            services.AddCustomResourceDefinitionUpdater<V1alpha1HelloWorld>(options =>
            {
                options.Scope = "Namespaced";
            });

            services.AddOperator<V1alpha1HelloWorld>(settings =>
            {
                settings
                    .WithRelatedResource<V1Deployment>()
                    .WithRelatedResource<V1ServiceAccount>()
                    .WithRelatedResource<V1Service>();

                settings.WithGenerator<HelloWorldGenerator>();
            });
        });

        await hostBuilder.RunConsoleAsync();
        return 0;
    }
}
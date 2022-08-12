using k8s;
using k8s.Models;
using Kubernetes.Controller.Informers;
using Kubernetes.Core;
using Microsoft.Extensions.Options;

namespace Kubernetes.Operator;

public class ConfigureOperatorOptions<TOperatorResource, TRelatedResource> : IConfigureNamedOptions<OperatorOptions>
    where TRelatedResource : class, IKubernetesObject<V1ObjectMeta>, new()
{
    private static GroupApiVersionKind _names = GroupApiVersionKind.From<TOperatorResource>();
    private readonly IResourceInformer<TRelatedResource> _resourceInformer;

    public ConfigureOperatorOptions(IResourceInformer<TRelatedResource> resourceInformer)
    {
        _resourceInformer = resourceInformer;
    }

    public void Configure(string name, OperatorOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.Equals(name, _names.PluralNameGroup, StringComparison.Ordinal))
        {
            options.Informers.Add(_resourceInformer);
        }
    }

    public void Configure(OperatorOptions options)
    {
    }
}
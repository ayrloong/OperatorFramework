using k8s;
using k8s.Models;

namespace Kubernetes.Operator;

public interface IOperatorHandler<TResource> : IDisposable
    where TResource : class, IKubernetesObject<V1ObjectMeta>
{
    Task RunAsync(CancellationToken cancellationToken);
}
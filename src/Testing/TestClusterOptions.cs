using k8s;
using k8s.Models;

namespace Kubernetes.Testing;

public class TestClusterOptions
{
    public IList<IKubernetesObject<V1ObjectMeta>> InitialResources { get; } = new List<IKubernetesObject<V1ObjectMeta>>();
}
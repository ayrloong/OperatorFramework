using Kubernetes.Core.Client;

namespace k8s;

public static class KubernetesHelpersExtensions
{
    public static IAnyResourceKind AnyResourceKind(this IKubernetes client)
    {
        return new AnyResourceKind(client);
    }
}

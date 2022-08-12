using k8s;
using k8s.Models;
using Kubernetes.Core;

namespace Kubernetes.Operator.Reconcilers;

public class ReconcileParameters<TResource> where TResource : class, IKubernetesObject<V1ObjectMeta>
{
    public ReconcileParameters(TResource resource,
        IDictionary<GroupKindNamespacedName, IKubernetesObject<V1ObjectMeta>> relatedResources)
    {
        Resource = resource;
        RelatedResources = relatedResources ?? throw new ArgumentNullException(nameof(relatedResources));
    }

    public TResource Resource { get; }
    public IDictionary<GroupKindNamespacedName, IKubernetesObject<V1ObjectMeta>> RelatedResources { get; }
}
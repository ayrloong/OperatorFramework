using Kubernetes.Core.ResourceKinds;

namespace Kubernetes.Core.Resources;

public class CreatePatchParameters
{
    public IResourceKind ResourceKind { get; set; }
    public object ApplyResource { get; set; }
    public object LastAppliedResource { get; set; }
    public object LiveResource { get; set; }
}
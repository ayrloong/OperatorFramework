namespace Kubernetes.Core.ResourceKinds;

public interface IResourceKind
{
    string ApiVersion { get; }

    string Kind { get; }

    IResourceKindElement Schema { get; }
}
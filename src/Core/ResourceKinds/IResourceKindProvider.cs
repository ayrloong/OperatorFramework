namespace Kubernetes.Core.ResourceKinds;

public interface IResourceKindProvider
{
    public Task<IResourceKind> GetResourceKindAsync(string apiVersion, string kind);
}
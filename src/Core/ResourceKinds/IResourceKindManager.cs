namespace Kubernetes.Core.ResourceKinds;

public interface IResourceKindManager
{
    public Task<IResourceKind> GetResourceKindAsync(string apiVersion, string kind);
}
namespace Kubernetes.Core.ResourceKinds;

public interface IResourceKindElement
{
    ElementMergeStrategy MergeStrategy { get; }

    public string MergeKey { get; }

    IResourceKindElement GetPropertyElementType(string name);

    IResourceKindElement GetCollectionElementType();       
}
namespace Kubernetes.CustomResources;

public class CustomResourceDefinitionUpdaterOptions<TResource> : CustomResourceDefinitionUpdaterOptions
{
}

public class CustomResourceDefinitionUpdaterOptions
{
    public string Scope { get; set; }
}
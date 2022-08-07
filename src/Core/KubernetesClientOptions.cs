using k8s;

namespace Kubernetes.Core;

/// <summary>
/// Class KubernetesClientOptions.
/// </summary>
public class KubernetesClientOptions
{
    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    public KubernetesClientConfiguration Configuration { get; set; }
}
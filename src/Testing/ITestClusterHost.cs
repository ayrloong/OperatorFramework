using k8s;
using k8s.KubeConfigModels;
using Microsoft.Extensions.Hosting;

namespace Kubernetes.Testing;

public interface ITestClusterHost : IHost
{
    K8SConfiguration KubeConfig { get; }

    IKubernetes Client { get; }

    ITestCluster Cluster { get; }
}
using k8s;
using k8s.Models;
using Microsoft.Rest;

namespace Kubernetes.Core.Client;

public class AnyResourceKind : IAnyResourceKind
{
    public AnyResourceKind(IKubernetes kubernetes)
    {
        Client = (k8s.Kubernetes)kubernetes;
    }
    public k8s.Kubernetes Client { get; }
    
    private static string Pattern(string group, string ns)
    {
        if (string.IsNullOrEmpty(group))
        {
            return string.IsNullOrEmpty(ns) ? "api/{version}/{plural}" : "api/{version}/namespaces/{namespace}/{plural}";
        }
        else
        {
            return string.IsNullOrEmpty(ns) ? "apis/{group}/{version}/{plural}" : "apis/{group}/{version}/namespaces/{namespace}/{plural}";
        }
    }
    
    public Task<HttpOperationResponse<KubernetesList<TResource>>>
        ListClusterAnyResourceKindWithHttpMessagesAsync<TResource>(string @group, string version, string plural,
            string continueParameter = null, string fieldSelector = null, string labelSelector = null,
            int? limit = null,
            string resourceVersion = null, int? timeoutSeconds = null, bool? watch = null, string pretty = null,
            Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default)
        where TResource : IKubernetesObject
    {
        throw new NotImplementedException();
    }

    public Task<HttpOperationResponse<object>> CreateAnyResourceKindWithHttpMessagesAsync<TResource>(TResource body,
        string @group, string version,
        string namespaceParameter, string plural, string dryRun = default(string),
        string fieldManager = default(string),
        string pretty = default(string), Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResource : IKubernetesObject
    {
        throw new NotImplementedException();
    }

    public Task<HttpOperationResponse<object>> PatchAnyResourceKindWithHttpMessagesAsync(V1Patch body, string @group,
        string version, string namespaceParameter,
        string plural, string name, string dryRun = default(string), string fieldManager = default(string),
        bool? force = default(bool?), Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public Task<HttpOperationResponse<object>> DeleteAnyResourceKindWithHttpMessagesAsync(string @group, string version,
        string namespaceParameter, string plural,
        string name, V1DeleteOptions body = default(V1DeleteOptions), int? gracePeriodSeconds = default(int?),
        bool? orphanDependents = default(bool?), string propagationPolicy = default(string),
        string dryRun = default(string), Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }
}
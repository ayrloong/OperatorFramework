using Kubernetes.Testing.Models;
using Microsoft.AspNetCore.Http;

namespace Kubernetes.Testing;

public interface ITestCluster
{
    Task UnhandledRequest(HttpContext context);

    Task<ListResult> ListResourcesAsync(string group, string version, string plural, ListParameters parameters);
}
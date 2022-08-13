using k8s.Models;
using Kubernetes.Testing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kubernetes.Testing.Controllers;

[Route("apis/{group}/{version}/{plural}")]
public class ResourceApiGroupController : Controller
{
    private readonly ITestCluster _testCluster;

    public ResourceApiGroupController(ITestCluster testCluster)
    {
        _testCluster = testCluster;
    }

    [FromRoute] public string Group { get; set; }

    [FromRoute] public string Version { get; set; }

    [FromRoute] public string Plural { get; set; }

    [HttpGet]
    public async Task<IActionResult> ListAsync(ListParameters parameters)
    {
        var list = await _testCluster.ListResourcesAsync(Group, Version, Plural, parameters);

        var result = new KubernetesList<ResourceObject>(
            apiVersion: $"{Group}/{Version}",
            kind: "DeploymentList",
            metadata: new V1ListMeta(
                continueProperty: list.Continue,
                remainingItemCount: null,
                resourceVersion: list.ResourceVersion),
            items: list.Items);

        return new ObjectResult(result);
    }
}
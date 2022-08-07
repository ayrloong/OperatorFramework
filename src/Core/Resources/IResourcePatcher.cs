using Microsoft.AspNetCore.JsonPatch;

namespace Kubernetes.Core.Resources;

public interface IResourcePatcher
{
    JsonPatchDocument CreateJsonPatch(CreatePatchParameters parameters);

}
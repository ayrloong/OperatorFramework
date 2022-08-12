using k8s;
using k8s.Models;
using Kubernetes.Controller.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kubernetes.Operator;

public class OperatorHostedService<TResource> : BackgroundHostedService
    where TResource : class, IKubernetesObject<V1ObjectMeta>, new()
{
    private readonly IOperatorHandler<TResource> _handler;

    public OperatorHostedService(
        IOperatorHandler<TResource> handler,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<OperatorHostedService<TResource>> logger)
        : base(hostApplicationLifetime, logger)
    {
        _handler = handler;
    }

    public override Task RunAsync(CancellationToken cancellationToken)
    {
        return _handler.RunAsync(cancellationToken);
    }
}
using Kubernetes.Controller.Informers;
using Kubernetes.Controller.Queues;
using Kubernetes.Controller.Rate;
using Kubernetes.Controller.RateLimiters;
using Kubernetes.Core;

namespace Kubernetes.Operator;

public class OperatorOptions
{

    /// <summary>
    /// Gets or sets factory for new ratelimitingqueue.
    /// </summary>
    /// <value>The new rate limiting queue.</value>
    public Func<IRateLimiter<NamespacedName>, IRateLimitingQueue<NamespacedName>> NewRateLimitingQueue { get; set; } =
        NewRateLimitingQueueDefault;

    /// <summary>
    /// Gets or sets factory for new ratelimiter.
    /// </summary>
    /// <value>The new rate limiter.</value>
    public Func<IRateLimiter<NamespacedName>> NewRateLimiter { get; set; } = NewRateLimiterDefault;

    /// <summary>
    /// Gets the informers.
    /// </summary>
    /// <value>
    /// The resource informers which have been registered for a given operator.
    /// </value>
    public List<IResourceInformer> Informers { get; } = new List<IResourceInformer>();

    /// <summary>
    /// NewRateLimitingQueueDefault is the default factory method of a rate limiting work queue.
    /// </summary>
    /// <param name="rateLimiter">The rate limiter.</param>
    /// <returns></returns>
    private static IRateLimitingQueue<NamespacedName> NewRateLimitingQueueDefault(
        IRateLimiter<NamespacedName> rateLimiter)
    {
        return new RateLimitingQueue<NamespacedName>(rateLimiter);
    }

    /// <summary>
    /// NewRateLimiterDefault is the default factory method of a rate limiter for a workqueue.  It has
    /// both overall and per-item rate limiting.  The overall is a token bucket and the per-item is exponential
    /// https://github.com/kubernetes/client-go/blob/master/util/workqueue/default_rate_limiters.go#L39.
    /// </summary>
    /// <returns>IRateLimiter&lt;NamespacedName&gt;.</returns>
    private static IRateLimiter<NamespacedName> NewRateLimiterDefault()
    {
        return new MaxOfRateLimiter<NamespacedName>(
            new BucketRateLimiter<NamespacedName>(
                limiter: new Limiter(
                    limit: new Limit(perSecond: 10),
                    burst: 100)),
            new ItemExponentialFailureRateLimiter<NamespacedName>(
                baseDelay: TimeSpan.FromMilliseconds(5),
                maxDelay: TimeSpan.FromSeconds(10)));
    }
}
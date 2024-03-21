using System;
using AppCoreNet.Diagnostics;

namespace AppCoreNet.EventStore.Subscription;

/// <summary>
/// Represents the result of <see cref="ISubscriptionStore.WatchAsync"/>.
/// </summary>
public sealed class WatchSubscriptionResult
{
    /// <summary>
    /// Gets the ID of the subscription.
    /// </summary>
    public SubscriptionId SubscriptionId { get; }

    /// <summary>
    /// Gets the ID of the subscribed stream.
    /// </summary>
    public StreamId StreamId { get; }

    /// <summary>
    /// Gets the position of the next event to be processed by the subscription.
    /// </summary>
    /// <remarks>
    /// The position refers to the <see cref="EventMetadata.StreamPosition"/> when the subscription was created
    /// for a specific stream. If the subscription was created for a wildcard stream it refers to the
    /// <see cref="EventMetadata.GlobalPosition"/>.
    /// </remarks>
    public long Position { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WatchSubscriptionResult"/> class.
    /// </summary>
    /// <param name="subscriptionId">The ID of the subscription.</param>
    /// <param name="streamId">The ID of the subscribed stream.</param>
    /// <param name="position">The position of the next event to be processed by the subscription.</param>
    public WatchSubscriptionResult(SubscriptionId subscriptionId, StreamId streamId, long position)
    {
        Ensure.Arg.NotNull(subscriptionId);
        Ensure.Arg.NotNull(streamId);
        Ensure.Arg.InRange(position, 0, long.MaxValue);

        if (subscriptionId.IsWildcard)
            throw new ArgumentException("Subscription ID must not contain wildcard characters.", nameof(subscriptionId));

        SubscriptionId = subscriptionId;
        StreamId = streamId;
        Position = position;
    }
}
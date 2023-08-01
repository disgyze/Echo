using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;

namespace Echo.Core.Capabilities
{
    //public sealed class ImmutableCapabilityCache : CapabilityCache
    //{
    //    ImmutableDictionary<XmppAddress, CapabilityCollection> capabilitiesCollection;
    //    Event<CapabilitiesAddedEventArgs> capabilitiesAdded;
    //    Event<CapabilitiesRemovedEventArgs> capabilitiesRemoved;

    //    public ImmutableCapabilityCache(EventChannel eventChannel, ImmutableDictionary<XmppAddress, CapabilityCollection>? capabilitiesCollection = null)
    //    {
    //        if (eventChannel is null)
    //        {
    //            throw new ArgumentNullException(nameof(eventChannel));
    //        }

    //        this.capabilitiesCollection = capabilitiesCollection ?? ImmutableDictionary<XmppAddress, CapabilityCollection>.Empty;

    //        capabilitiesAdded = eventChannel.GetEvent<CapabilitiesAddedEventArgs>();
    //        capabilitiesRemoved = eventChannel.GetEvent<CapabilitiesRemovedEventArgs>();
    //    }

    //    public override ValueTask SaveOrUpdateCapabilitiesAsync(XmppAddress address, CapabilityCollection capabilities, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (address is null)
    //        {
    //            throw new ArgumentNullException(nameof(address));
    //        }

    //        ImmutableInterlocked.AddOrUpdate(ref capabilitiesCollection, address, capabilities, (address, oldCapabilities) => capabilities);

    //        return ValueTask.CompletedTask;
    //    }

    //    public override ValueTask<bool> DeleteCapabilitiesAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();
    //        return ValueTask.FromResult(ImmutableInterlocked.TryRemove(ref capabilitiesCollection, address, out _));
    //    }

    //    public override ValueTask<bool> ClearAsync(CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();
    //        return ValueTask.FromResult(ImmutableInterlocked.Update(ref capabilitiesCollection, collection => collection.Clear()));
    //    }

    //    public override ValueTask<CapabilityCollection?> GetCapabilitiesAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();
    //        capabilitiesCollection.TryGetValue(address, out var capabilities);
    //        return ValueTask.FromResult(capabilities);
    //    }
    //}
}
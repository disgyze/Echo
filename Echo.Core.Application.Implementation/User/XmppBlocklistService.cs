namespace Echo.Core.User
{
    //public sealed class XmppBlocklistService : BlocklistService
    //{
    //    XmppClient connection;
    //    EventService eventService;

    //    public XmppBlocklistService(XmppClient connection, EventService eventService)
    //    {
    //        this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
    //        this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    //    }

    //    public override async ValueTask<ImmutableArray<XmppAddress>> GetBlockedAddressesAsync(CancellationToken cancellationToken = default)
    //    {
    //        using var request = new XmppStanzaRequest<XmppIQ>(connection, eventService);
    //        var requestStanza = new XmppIQ(XmppIQKind.Get, XmppBlocklist.Empty);

    //        if (await request.GetResponseAsync(requestStanza, cancellationToken).ConfigureAwait(false) is XmppIQ responseStanza)
    //        {
    //            if (responseStanza.Element(XmppBlocklist.ElementName) is XmppBlocklist blocklist)
    //            {
    //                return ImmutableArray.CreateRange(blocklist.Items.Select(item => XmppAddress.Create(item.Jid)));
    //            }
    //        }

    //        return ImmutableArray<XmppAddress>.Empty;
    //    }

    //    public override async ValueTask<bool> BlockAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        if (address is null)
    //        {
    //            throw new ArgumentNullException(nameof(address));
    //        }

    //        using var request = new XmppStanzaRequest<XmppIQ>(connection, eventService);
    //        var requestStanza = new XmppIQ(XmppIQKind.Set, new XmppBlock(new XmppBlocklistItem(address)));
    //        var responseStanza = await request.GetResponseAsync(requestStanza, cancellationToken).ConfigureAwait(false);

    //        return responseStanza is not null && responseStanza.Kind != XmppIQKind.Error;
    //    }

    //    public override async ValueTask<bool> UnblockAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        if (address is null)
    //        {
    //            throw new ArgumentNullException(nameof(address));
    //        }

    //        using var request = new XmppStanzaRequest<XmppIQ>(connection, eventService);
    //        var requestStanza = new XmppIQ(XmppIQKind.Set, new XmppUnblock(new XmppBlocklistItem(address)));
    //        var responseStanza = await request.GetResponseAsync(requestStanza, cancellationToken).ConfigureAwait(false);

    //        return responseStanza is not null && responseStanza.Kind != XmppIQKind.Error;
    //    }

    //    public override async ValueTask<bool> UnblockAllAsync(CancellationToken cancellationToken = default)
    //    {
    //        using var request = new XmppStanzaRequest<XmppIQ>(connection, eventService);
    //        var requestStanza = new XmppIQ(XmppIQKind.Set, XmppUnblock.Empty);
    //        var responseStanza = await request.GetResponseAsync(requestStanza, cancellationToken).ConfigureAwait(false);

    //        return responseStanza is not null && responseStanza.Kind != XmppIQKind.Error;
    //    }
    //}
}
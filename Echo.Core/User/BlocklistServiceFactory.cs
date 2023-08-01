using System;
using Echo.Core.Connections;
using Echo.Core.Extensibility.Eventing;

namespace Echo.Core.User
{
    public sealed class BlocklistServiceFactory
    {
        EventService eventService;

        internal BlocklistServiceFactory(EventService eventService) 
        {
            this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        public BlocklistService Create(XmppConnectionService connection)
        {
            throw new NotImplementedException();
        }
    }
}
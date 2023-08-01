using System;
using Echo.Core.Messaging;
using Echo.Core.User;
using Echo.Core.Capabilities;
using Echo.Core.Connections;

namespace Echo.Core.Extensibility.Eventing
{
    internal sealed class ImmutableEventChannel : EventChannel
    {
        // TODO Add events
        object[] eventCollection = new object[]
        {
            new Event<AccountAddedEventArgs>(),
            new Event<AccountRemovedEventArgs>(),
            new Event<AccountChangedEventArgs>(),

            new Event<ContactAddedEventArgs>(),
            new Event<ContactRemovedEventArgs>(),
            new Event<ContactChangedEventArgs>(),

            new Event<CapabilitiesAddedEventArgs>(),
            new Event<CapabilitiesRemovedEventArgs>(),

            new Event<ActiveConnectionChangedEventArgs>(),
            new Event<XmppConnectionServiceStateChangedEventArgs>(),
            new Event<XmppConnectionServiceErrorEventArgs>(),
            new Event<XmppConnectionServiceFailedEventArgs>(),
            new Event<XmppConnectionServiceAddedEventArgs>(),
            new Event<XmppConnectionServiceConnectionRemovedEventArgs>(),
            new Event<DnsResolvingEventArgs>(),
            new Event<DnsResolutionFailedEventArgs>(),
            new Event<DnsResolutionSucceedEventArgs>(),
            new Event<CertificateValidatingEventArgs>(),
            new Event<CertificateValidationFailedEventArgs>(),
            new Event<CertificateValidationSucceedEventArgs>(),
            new Event<PingSentEventArgs>(),
            new Event<PingReceivedEventArgs>(),
            new Event<XmlElementSentEventArgs>(),
            new Event<XmlElementReceivedEventArgs>(),
            new Event<XmlParsingFailedEventArgs>(),
            new Event<XmlStreamClosedEventArgs>(),
            new Event<XmlStreamOpenedEventArgs>(),

            new Event<ConversationMessageReceivedEventArgs>(),
            new Event<ConversationHistoryMessageReceivedEventArgs>(),
            new Event<ParticipantNickChangedEventArgs>(),
            new Event<ParticipantStatusChangedEventArgs>(),
            new Event<ParticipantJoinedEventArgs>(),
            new Event<ParticipantLeftEventArgs>(),
            new Event<ParticipantKickedEventArgs>(),
            new Event<ParticipantBannedEventArgs>()
        };

        public override Event<TEventArgs> GetEvent<TEventArgs>()
        {
            for (int i = 0; i < eventCollection.Length; i++)
            {
                if (eventCollection[i] is Event<TEventArgs> @event)
                {
                    return @event;
                }
            }
            throw new InvalidOperationException($"{nameof(EventChannel)}.{nameof(EventChannel.GetEvent)}: event of type '{typeof(TEventArgs).Name}' is not found");
        }
    }
}
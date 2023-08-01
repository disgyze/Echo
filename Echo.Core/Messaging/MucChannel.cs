using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Connections;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Messaging
{
    public sealed class MucChannel : Channel
    {
        public string Name { get; }
        public string? Subject { get; internal set; }
        public MucParticipant? Me { get; internal set; }
        public MucChannelConfiguration Configuration { get; internal set; }
        public new IReadOnlyList<MucParticipant> Participants => (IReadOnlyList<MucParticipant>)base.Participants;

        internal MucChannel(string name, XmppAddress address, XmppConnectionService connection) : base(address, connection)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public async ValueTask<MucChannelLeaveResult> LeaveAsync(string? reason = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (reason is null)
            {
                throw new ArgumentNullException(nameof(reason));
            }

            if (!IsJoined)
            {
                return MucChannelLeaveResult.NotJoined;
            }

            if (Connection.State != XmppConnectionServiceState.Opened)
            {
                return MucChannelLeaveResult.NotConnected;
            }

            if (Me is MucParticipant me)
            {
                var presence = new XmppPresence(
                    sender: Connection.Account.Address,
                    target: me.Address,
                    statusText: reason,
                    kind: XmppPresenceKind.Unavailable);

                if (await Connection.SendAsync(presence, cancellationToken).ConfigureAwait(false))
                {
                    return MucChannelLeaveResult.NotConnected;
                }

                return MucChannelLeaveResult.Success;
            }

            return MucChannelLeaveResult.NotJoined;
        }
    }
}
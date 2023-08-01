using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;
using Echo.Core.User;
using Echo.Xmpp.Connections;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections.Authentication
{
    public abstract class AuthenticationService : IDisposable
    {
        bool disposed = false;
        Xmpp.Connections.XmppConnection connection;
        EventService eventService;
        AccountCredential? credential;
        TaskCompletionSource<AuthenticationResult>? completionSource;
        IDisposable? xmlElementReceivedToken;

        protected AuthenticationService(Xmpp.Connections.XmppConnection connection, EventService eventService)
        {
            this.connection = connection;
            this.eventService = eventService;

            xmlElementReceivedToken = eventService.RegisterEvent<XmlElementReceivedEventArgs>(EventXmlElementReceived);
        }

        ~AuthenticationService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                xmlElementReceivedToken?.Dispose();
                xmlElementReceivedToken = null;
            }

            disposed = true;
        }

        protected abstract XmppSaslAuth GetXmppSaslAuth();
        protected abstract XmppSaslResponse GetXmppSaslResponse(AccountCredential? credential, XmppSaslChallenge challenge);

        public virtual async ValueTask<AuthenticationResult> AuthenticateAsync(AccountCredential credential, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            this.credential = credential;
            completionSource = new TaskCompletionSource<AuthenticationResult>();

            //if (!await connection.SendAsync(GetXmppSaslAuth()).ConfigureAwait(false))
            //{
            //    return AuthenticationResult.Failure;
            //}

            return await completionSource.Task;
        }

        private async ValueTask<EventResult> EventXmlElementReceived(XmlElementReceivedEventArgs e)
        {
            switch (e.Element)
            {
                case XmppSaslSuccess:
                {
                    completionSource?.SetResult(AuthenticationResult.Success);
                    break;
                }

                case XmppSaslFailure:
                {
                    completionSource?.SetResult(AuthenticationResult.Failure);
                    break;
                }

                case XmppSaslChallenge challenge:
                {
                    //if (!await connection.SendAsync(GetXmppSaslResponse(credential, challenge)))
                    //{
                    //    completionSource?.SetResult(AuthenticationResult.Failure);
                    //}
                    break;
                }
            }
            return EventResult.Stop;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(AuthenticationService));
            }
        }
    }
}
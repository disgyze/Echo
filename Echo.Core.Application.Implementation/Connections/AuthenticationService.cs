using System;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Foundation;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections
{
    public abstract class AuthenticationService : IAuthenticationService, IDisposable, IAsyncDisposable
    {
        bool disposed = false;
        DisposableContainer? subscriptionList;
        TaskCompletionSource<AuthenticationResult>? completionSource;
        IEventService? eventService;
        IXmppConnection? connection;

        public IXmppConnection? Connection => connection;
        public TaskCompletionSource<AuthenticationResult>? CompletionSource => completionSource;

        protected AuthenticationService(IXmppConnection connection, IEventService eventService)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        public void Dispose()
        {
            var task = DisposeAsync();

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public ValueTask DisposeAsync()
        {
            if (!disposed)
            {
                subscriptionList?.Dispose();

                connection = null;
                completionSource = null;
                
                disposed = true;
            }
            return default;
        }

        public ValueTask<AuthenticationResult> AuthenticateAsync()
        {
            ThrowIfDisposed();

            completionSource = new TaskCompletionSource<AuthenticationResult>();
            subscriptionList = new DisposableContainer(eventService!.RegisterEvent<XmlElementReceivedEventArgs>(EventXmlElementReceived));

            _ = OnInitialStateAsync();

            return new ValueTask<AuthenticationResult>(completionSource.Task);
        }

        private async ValueTask<EventResult> EventXmlElementReceived(XmlElementReceivedEventArgs e)
        {
            switch (e.Element)
            {
                case XmppSaslChallenge challenge:
                {
                    await OnChallengeStateAsync(challenge).ConfigureAwait(false);
                    return EventResult.Stop;
                }

                case XmppSaslSuccess success:
                {
                    await OnSuccessStateAsync(success).ConfigureAwait(false);
                    return EventResult.Stop;
                }

                case XmppSaslFailure failure:
                {
                    await OnFailureStateAsync(failure).ConfigureAwait(false);
                    return EventResult.Stop;
                }
            }
            return EventResult.Continue;
        }

        protected abstract ValueTask OnInitialStateAsync();
        protected abstract ValueTask OnChallengeStateAsync(XmppSaslChallenge saslChallenge);

        private ValueTask OnSuccessStateAsync(XmppSaslSuccess success)
        {
            completionSource!.SetResult(AuthenticationResult.Success);
            return default;
        }

        private ValueTask OnFailureStateAsync(XmppSaslFailure failure)
        {
            completionSource!.SetResult(AuthenticationResult.Failure);
            return default;
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
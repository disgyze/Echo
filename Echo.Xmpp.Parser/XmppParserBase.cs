using System;

namespace Echo.Xmpp.Parser
{
    public abstract class XmppParserBase : IXmppParser
    {
        public event EventHandler StreamStarted;
        public event EventHandler StreamClosed;
        public event EventHandler BufferOverflowError;
        public event EventHandler<XmppParsingFailedEventArgs> ParsingFailed;
        public event EventHandler<XmppElementEventArgs> ElementParsed;

        public int Depth
        {
            get;
            protected set;
        }

        public int MaxBufferSize
        {
            get;
            set;
        }

        protected virtual void OnStreamStart()
        {
            StreamStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStreamEnd()
        {
            StreamClosed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnBufferOverflowError()
        {
            BufferOverflowError?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnParsingError(XmppParsingFailedEventArgs e)
        {
            ParsingFailed?.Invoke(this, e);
        }

        protected virtual void OnElementParsed(XmppElementEventArgs e)
        {
            ElementParsed?.Invoke(this, e);
        }

        public abstract void Parse(byte[] data);
        public abstract void Parse(ReadOnlySpan<byte> data);
        public abstract void Cancel();
        public abstract void Reset();
    }
}
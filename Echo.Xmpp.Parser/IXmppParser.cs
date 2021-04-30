using System;

namespace Echo.Xmpp.Parser
{
    public interface IXmppParser
    {
        event EventHandler StreamStarted;
        event EventHandler StreamClosed;
        event EventHandler BufferOverflowError;
        event EventHandler<XmppElementEventArgs> ElementParsed;
        event EventHandler<XmppParsingFailedEventArgs> ParsingFailed;

        int MaxBufferSize { get; set; }
        int Depth { get; }

        void Parse(byte[] data);
        void Parse(ReadOnlySpan<byte> data);
        void Reset();
        void Cancel();
    }
}
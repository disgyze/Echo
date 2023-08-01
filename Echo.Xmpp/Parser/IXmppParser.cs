using System;

namespace Echo.Xmpp.Parser
{
    public interface IXmppParser
    {
        event XmppParserEventHandler<XmppParserDeclarationParsedEventArgs>? DeclarationParsed;
        event XmppParserEventHandler<XmppParserElementParsedEventArgs>? ElementParsed;
        event XmppParserEventHandler<XmppParsingFailedEventArgs>? ParsingFailed;
        event XmppParserEventHandler? StreamClosed;
        event XmppParserEventHandler? StreamOpened;

        void Parse(ReadOnlySpan<byte> data);
        void Reset();
    }
}
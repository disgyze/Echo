using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public readonly struct XmppParserDeclarationParsedEventArgs
    {
        public XDeclaration Declaration { get; }

        public XmppParserDeclarationParsedEventArgs(XDeclaration declaration)
        {
            Declaration = declaration;
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Echo.Core.Connections
{
    public sealed class XmppStream : IXmppStream
    {
        Stream stream;

        const string streamOpen = "<?xml version='1.0'?><stream:stream to='{0}' version='1.0' xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams";
        const string streamClose = "</stream:stream>";

        public XmppStream(Stream stream)
        {
            this.stream = stream;
        }

        public ValueTask CloseAsync()
        {
            return stream.WriteAsync(Encoding.UTF8.GetBytes(streamClose).AsMemory());
        }

        public ValueTask OpenAsync(string domain)
        {
            return stream.WriteAsync(Encoding.UTF8.GetBytes(string.Format(streamOpen, domain)).AsMemory());
        }

        public ValueTask WriteAsync(XElement element)
        {
            return stream.WriteAsync(Encoding.UTF8.GetBytes(element.ToString()).AsMemory());
        }
    }
}
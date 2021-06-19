using System;
using Echo.Core.Connections;
using Echo.Core.UI;

namespace Echo.Core.Extensibility
{
    public sealed class CommandArgs
    {
        public string Params { get; }
        public IWindow Window { get; }
        public IXmppConnection Connection { get; }

        public CommandArgs(IXmppConnection connection, IWindow window, string @params)
        {
            Connection = connection;
            Window = window;
            Params = @params;
        }
    }
}
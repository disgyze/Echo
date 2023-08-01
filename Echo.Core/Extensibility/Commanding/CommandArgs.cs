using Echo.Core.Connections;
using Echo.Core.UI;

namespace Echo.Core.Extensibility.Commanding
{
    public readonly struct CommandArgs
    {
        public XmppConnectionService Connection { get; }
        public IWindow Window { get; }
        public string Params { get; }

        public CommandArgs(XmppConnectionService connection, IWindow window, string @params)
        {
            Connection = connection;
            Window = window;
            Params = @params ?? string.Empty;
        }
    }
}
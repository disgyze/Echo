using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echo.Core.Connections;

namespace Echo.Core.UI
{
    public interface IWindowFactory
    {
        IWindow CreateConnectionWindow(IXmppConnection connection);
    }
}
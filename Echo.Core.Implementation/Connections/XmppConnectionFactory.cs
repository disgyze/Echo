using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public sealed class XmppConnectionFactory : IXmppConnectionFactory
    {
        IWindowFactory windowFactory;

        public IXmppConnection Create(IAccount account)
        {
            return new DefaultXmppConnection(account, null, null);
        }
    }
}
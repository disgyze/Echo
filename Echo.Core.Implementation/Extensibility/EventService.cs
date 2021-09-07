using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public sealed class EventService : IEventService
    {
        Guid pluginId;

        public IDisposable RegisterEvent<TEventArgs>(Func<TEventArgs, ValueTask<EventResult>> handler)
        {
            return null;
        }
    }
}
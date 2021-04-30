using System.Collections.Generic;
using Echo.Core.Metadata;

namespace Echo.Core.Messaging
{
    public interface IMessage
    {
        string Text { get; }
        IReadOnlyList<IMetadata> Metadata { get; }
    }
}
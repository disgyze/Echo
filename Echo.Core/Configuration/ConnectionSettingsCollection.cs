using System;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Core.Configuration
{
    public sealed class ConnectionSettingsCollection
    {
        IEnumerable<ConnectionSettings> collection;

        public ConnectionSettingsCollection(IEnumerable<ConnectionSettings> collection) 
        { 
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public ConnectionSettings? GetFor(Guid accountId)
        {
            return collection.FirstOrDefault(settings => settings.AccountId == accountId);
        }
    }
}
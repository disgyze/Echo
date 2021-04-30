using System;

namespace Echo.Core.Extensibility
{
    public sealed class CommandArgs
    {
        public string Params { get; }
        public Guid WindowId { get; }

        public CommandArgs(Guid windowId, string @params)
        {
            WindowId = windowId;
            Params = @params;
        }
    }
}
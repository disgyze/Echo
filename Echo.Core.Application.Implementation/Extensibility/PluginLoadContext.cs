using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Echo.Core.Extensibility
{
    public sealed class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext() : base(isCollectible: true)
        {
        }

        public IPlugin Create()
        {
            throw new NotImplementedException();
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}
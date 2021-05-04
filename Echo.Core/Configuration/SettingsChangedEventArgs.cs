using System;

namespace Echo.Core.Configuration
{
    public abstract class SettingsChangedEventArgs<TSettings> : EventArgs
    {
        public TSettings? OldSettings { get; }
        public TSettings? NewSettings { get; }

        protected SettingsChangedEventArgs(TSettings? oldSettings, TSettings? newSettings)
        {
            OldSettings = oldSettings;
            NewSettings = newSettings;
        }
    }
}
namespace Echo.Configuration
{
    public abstract class ConfigurationBinder<TSettings>
    {
        public abstract TSettings Bind(ConfigurationNode node);
        public abstract ConfigurationNode Bind(TSettings settings);
    }
}
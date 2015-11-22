using CorrelatorSharp.Logging.NLog;

namespace CorrelatorSharp.Logging
{
    public static class LoggingConfigurationExtensions
    {
        public static LoggingConfiguration UseNLog(this LoggingConfiguration config)
        {
            config.WithLogManager(new LogManagerAdaptor());
            return config;
        }
    }
}

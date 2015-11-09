using CorrelatorSharp.Logging.NLog;

namespace CorrelatorSharp.Logging
{
    public static class ActivityLoggerConfigurationExtensions
    {
        public static LoggingConfiguration UseNLog(this LoggingConfiguration config)
        {
            config.WithLogManager(new LogManagerAdaptor());
            return config;
        }
    }
}

using ActivityLogger.Logging.NLog;

namespace ActivityLogger.Logging
{
    public static class ActivityLoggerConfigurationExtensions
    {
        public static ActivityLoggerConfiguration UseNLog(this ActivityLoggerConfiguration config)
        {
            config.WithLogManager(new LogManagerAdaptor());
            return config;
        }
    }
}

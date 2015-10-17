using System;

namespace ActivityLogger.Logging
{
    public static class LogManager
    {
        public static ILogger GetCurrentClassLogger()
        {
            return ActivityLoggerConfiguration.LogManager.GetCurrentClassLogger();
        }

        public static ILogger GetLogger(string name)
        {
            return ActivityLoggerConfiguration.LogManager.GetLogger(name);
        }

    }
}

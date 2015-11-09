using System;

namespace CorrelatorSharp.Logging
{
    public static class LogManager
    {
        public static ILogger GetCurrentClassLogger()
        {
            return LoggingConfiguration.LogManager.GetCurrentClassLogger();
        }

        public static ILogger GetLogger(string name)
        {
            return LoggingConfiguration.LogManager.GetLogger(name);
        }

    }
}

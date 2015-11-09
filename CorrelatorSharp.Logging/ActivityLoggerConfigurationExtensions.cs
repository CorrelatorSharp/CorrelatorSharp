using System;
using System.Linq;

namespace CorrelatorSharp.Logging
{
    public static class ActivityLoggerConfigurationExtensions
    {
        public static LoggingConfiguration WithLogManager(this LoggingConfiguration config, ILogManagerAdaptor adaptor)
        {
            LoggingConfiguration.LogManager = adaptor;
            return config;
        }
    }

}

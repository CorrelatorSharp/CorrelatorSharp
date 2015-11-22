using System;
using System.Linq;

namespace CorrelatorSharp.Logging
{
    public static class LoggingConfigurationExtensions
    {
        public static LoggingConfiguration WithLogManager(this LoggingConfiguration config, ILogManagerAdaptor adaptor)
        {
            LoggingConfiguration.LogManager = adaptor;
            return config;
        }
    }

}

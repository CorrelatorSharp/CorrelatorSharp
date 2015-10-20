using System;
using System.Linq;

namespace Correlator.Logging
{
    public static class ActivityLoggerConfigurationExtensions
    {
        public static ActivityLoggerConfiguration WithLogManager(this ActivityLoggerConfiguration config, ILogManagerAdaptor adaptor)
        {
            ActivityLoggerConfiguration.LogManager = adaptor;
            return config;
        }
    }

}

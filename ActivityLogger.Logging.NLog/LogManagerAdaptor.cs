using ActivityLogger.Logging;
using NLogLogManager = NLog.LogManager;

namespace ActivityLogger.Logging.NLog
{
    public class LogManagerAdaptor : ILogManagerAdaptor
    {
        public ILogger GetCurrentClassLogger()
        {
            return new LoggerAdaptor(NLogLogManager.GetCurrentClassLogger());
        }

        public ILogger GetLogger(string name)
        {
            return new LoggerAdaptor(NLogLogManager.GetLogger(name));
        }
    }
}

using System;

namespace CorrelatorSharp.Logging.Dummy
{
    internal class DummyLoggerAdaptor : ILogger
    {
        public readonly static DummyLoggerAdaptor Instance = new DummyLoggerAdaptor();

        public string Name {
            get {
                return "DummyLogger";
            }
        }

        public void LogError(string format, params object[] values)
        {
        }

        public void LogError(Exception exception, string format = "", params object[] values)
        {
        }

        public void LogFatal(string format, params object[] values)
        {
        }

        public void LogFatal(Exception exception, string format = "", params object[] values)
        {
        }

        public void LogInfo(string format, params object[] values)
        {
        }

        public void LogInfo(Exception exception, string format = "", params object[] values)
        {
        }

        public void LogTrace(string format, params object[] values)
        {
        }

        public void LogTrace(Exception exception, string format = "", params object[] values)
        {
        }

        public void LogWarning(string format, params object[] values)
        {
        }

        public void LogWarning(Exception exception, string format = "", params object[] values)
        {
        }
    }
}
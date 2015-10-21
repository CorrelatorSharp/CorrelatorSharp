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

        public bool IsTraceEnabled {
            get { return true; }
        }

        public bool IsWarnEnabled {
            get { return true; }
        }

        public bool IsInfoEnabled {
            get { return true; }
        }

        public bool IsErrorEnabled {
            get { return true; }
        }

        public bool IsFatalEnabled {
            get { return true; }
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

        public void LogWarn(string format, params object[] values)
        {
        }

        public void LogWarn(Exception exception, string format = "", params object[] values)
        {
        }
    }
}
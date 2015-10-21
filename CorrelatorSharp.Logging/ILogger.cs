using System;

namespace CorrelatorSharp.Logging
{

    public interface ILogger
    {

        string Name { get; }

        void LogError(string format, params object[] values);
        void LogError(Exception exception, string format = "", params object[] values);
        void LogFatal(string format, params object[] values);
        void LogFatal(Exception exception, string format = "", params object[] values);
        void LogInfo(string format, params object[] values);
        void LogInfo(Exception exception, string format = "", params object[] values);
        void LogTrace(string format, params object[] values);
        void LogTrace(Exception exception, string format = "", params object[] values);
        void LogWarn(string format, params object[] values);
        void LogWarn(Exception exception, string format = "", params object[] values);

        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
      
    }
}
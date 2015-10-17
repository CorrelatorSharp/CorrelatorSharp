using System;

namespace ActivityLogger.Logging
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
        void LogWarning(string format, params object[] values);
        void LogWarning(Exception exception, string format = "", params object[] values);
    }
}
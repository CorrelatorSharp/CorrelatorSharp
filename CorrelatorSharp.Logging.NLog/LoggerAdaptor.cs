using System;
using CorrelatorSharp;
using NLog;
using NLogILogger = NLog.ILogger;

namespace CorrelatorSharp.Logging.NLog
{
    public class LoggerAdaptor : Logging.ILogger
    {
        public const string ActivityIdPropertyName = "cs-activity-id";
        public const string ParentIdPropertyName = "cs-activity-parentid";
        public const string NamePropertyName = "cs-activity-name";

        private readonly NLogILogger _logger;


        public LoggerAdaptor(NLogILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger), $"{nameof(logger)} is null.");

            _logger = logger;
        }

        public string Name {
            get {
                return _logger.Name;
            }
        }

        public bool IsTraceEnabled {
            get { return _logger.IsTraceEnabled; }
        }

        public bool IsWarnEnabled {
            get { return _logger.IsWarnEnabled; }
        }

        public bool IsInfoEnabled {
            get { return _logger.IsInfoEnabled; }
        }

        public bool IsErrorEnabled {
            get { return _logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled {
            get { return _logger.IsFatalEnabled; }
        }

        private LogEventInfo CreateLogEntry(LogLevel level, string format, params object[] values)
        {
            LogEventInfo entry = new LogEventInfo(level, Name, null, format, values);

            ActivityScope currentActivity = ActivityScope.Current;
            if (currentActivity != null) {
                entry.Properties[ActivityIdPropertyName] = currentActivity.Id;
                entry.Properties[ParentIdPropertyName] = currentActivity.ParentId;
                entry.Properties[NamePropertyName] = currentActivity.Name;
            }

            return entry;
        }

        public void LogTrace(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsTraceEnabled) {

                LogEventInfo entry = CreateLogEntry(LogLevel.Trace, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogError(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsErrorEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Error, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogWarn(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsWarnEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Warn, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogInfo(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsInfoEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Info, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogFatal(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsFatalEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Fatal, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogFatal(string format, params object[] values)
        {
            if (_logger.IsFatalEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Fatal, format, values);

                _logger.Log(entry);
            }
        }

        public void LogWarn(string format, params object[] values)
        {
            if (_logger.IsWarnEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Warn, format, values);

                _logger.Log(entry);
            }
        }

        public void LogInfo(string format, params object[] values)
        {
            if (_logger.IsInfoEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Info, format, values);

                _logger.Log(entry);
            }
        }

        public void LogTrace(string format, params object[] values)
        {
            if (_logger.IsTraceEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Trace, format, values);

                _logger.Log(entry);
            }
        }

        public void LogError(string format, params object[] values)
        {
            if (_logger.IsErrorEnabled) {
                LogEventInfo entry = CreateLogEntry(LogLevel.Error, format, values);

                _logger.Log(entry);
            }
        }
    }
}
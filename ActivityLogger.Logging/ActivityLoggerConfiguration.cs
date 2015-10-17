using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityLogger.Logging
{
    public class ActivityLoggerConfiguration
    {
        public static readonly ActivityLoggerConfiguration Current = new ActivityLoggerConfiguration();

        private static readonly Lazy<ILogManagerAdaptor> _dummyLogManager = new Lazy<ILogManagerAdaptor>(() => Dummy.DummyLogManagerAdaptor.Instance);
        private static ILogManagerAdaptor _logManager;


        private ActivityLoggerConfiguration()
        {
        }


        internal static ILogManagerAdaptor LogManager {
            get {
                if (_logManager == null)
                    return _dummyLogManager.Value;

                return _logManager;
            }
            set {
                _logManager = value;
            }
        }
    }
}

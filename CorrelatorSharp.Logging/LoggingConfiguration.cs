using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelatorSharp.Logging
{
    public class LoggingConfiguration
    {
        public static readonly LoggingConfiguration Current = new LoggingConfiguration();

        private static readonly Lazy<ILogManagerAdaptor> _dummyLogManager = new Lazy<ILogManagerAdaptor>(() => Dummy.DummyLogManagerAdaptor.Instance);
        private static ILogManagerAdaptor _logManager;


        private LoggingConfiguration()
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

using System;
using System.Linq;

namespace Correlator.Logging.Dummy
{
    internal class DummyLogManagerAdaptor : ILogManagerAdaptor
    {
        public readonly static DummyLogManagerAdaptor Instance = new DummyLogManagerAdaptor();

        public ILogger GetCurrentClassLogger()
        {
            return DummyLoggerAdaptor.Instance;
        }

        public ILogger GetLogger(string name)
        {
            return DummyLoggerAdaptor.Instance;
        }
    }
}

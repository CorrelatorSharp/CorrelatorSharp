using System;
using System.Linq;

namespace Correlator.Logging
{

    public interface ILogManagerAdaptor
    {
        ILogger GetCurrentClassLogger();
        ILogger GetLogger(string name);
    }
}

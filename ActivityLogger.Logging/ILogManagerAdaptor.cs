using System;
using System.Linq;

namespace ActivityLogger.Logging
{

    public interface ILogManagerAdaptor
    {
        ILogger GetCurrentClassLogger();
        ILogger GetLogger(string name);
    }
}

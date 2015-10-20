using System;
using System.Linq;

namespace CorrelatorSharp.Logging
{

    public interface ILogManagerAdaptor
    {
        ILogger GetCurrentClassLogger();
        ILogger GetLogger(string name);
    }
}

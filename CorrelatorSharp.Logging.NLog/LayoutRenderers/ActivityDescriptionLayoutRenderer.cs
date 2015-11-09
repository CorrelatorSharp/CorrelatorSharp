using System;
using System.Linq;
using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace CorrelatorSharp.Logging.NLog.LayoutRenderers
{

    [LayoutRenderer(LoggerAdaptor.NamePropertyName)]
    public class ActivityDescriptionLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (!logEvent.Properties.ContainsKey(LoggerAdaptor.NamePropertyName))
                return;

            builder.Append(logEvent.Properties[LoggerAdaptor.NamePropertyName]);
        }
    }
}

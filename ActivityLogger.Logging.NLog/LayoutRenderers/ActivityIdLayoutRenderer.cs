using System;
using System.Linq;
using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace ActivityLogger.Logging.NLog.LayoutRenderers
{

    [LayoutRenderer(LoggerAdaptor.ActivityIdPropertyName)]
    public class ActivityIdLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (!logEvent.Properties.ContainsKey(LoggerAdaptor.ActivityIdPropertyName))
                return;

            builder.Append(logEvent.Properties[LoggerAdaptor.ActivityIdPropertyName]);
        }
    }
}

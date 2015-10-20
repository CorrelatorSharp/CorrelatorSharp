using System;
using System.Linq;
using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace CorrelatorSharp.Logging.NLog.LayoutRenderers
{

    [LayoutRenderer(LoggerAdaptor.ParentIdPropertyName)]
    public class ActivityParentLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (!logEvent.Properties.ContainsKey(LoggerAdaptor.ParentIdPropertyName))
                return;

            builder.Append(logEvent.Properties[LoggerAdaptor.ParentIdPropertyName]);
        }
    }
}

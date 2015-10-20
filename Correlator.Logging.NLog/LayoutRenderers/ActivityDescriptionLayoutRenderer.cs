using System;
using System.Linq;
using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Correlator.Logging.NLog.LayoutRenderers
{

    [LayoutRenderer(LoggerAdaptor.DescriptionPropertyName)]
    public class ActivityDescriptionLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (!logEvent.Properties.ContainsKey(LoggerAdaptor.DescriptionPropertyName))
                return;

            builder.Append(logEvent.Properties[LoggerAdaptor.DescriptionPropertyName]);
        }
    }
}

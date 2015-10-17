using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace ActivityLogger.Correlation.ApplicationInsights
{
    public class OperationIdTelementryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (ActivityLogScope.Current == null)
                return;

            telemetry.Context.Operation.Id = ActivityLogScope.Current.Id;
            telemetry.Context.Operation.Name = ActivityLogScope.Current.Name;

            if (!String.IsNullOrWhiteSpace(ActivityLogScope.Current.ParentId))
                telemetry.Context.Properties.Add("ParentOperationId", ActivityLogScope.Current.ParentId);
        }
    }
}

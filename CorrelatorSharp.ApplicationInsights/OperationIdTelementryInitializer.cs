using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace CorrelatorSharp.ApplicationInsights
{
    public class OperationIdTelementryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (ActivityScope.Current == null)
                return;

            telemetry.Context.Operation.Id = ActivityScope.Current.Id;
            telemetry.Context.Operation.Name = ActivityScope.Current.Name;

            if (!String.IsNullOrWhiteSpace(ActivityScope.Current.ParentId))
                telemetry.Context.Properties.Add("ParentOperationId", ActivityScope.Current.ParentId);
        }
    }
}

using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using System;

namespace MasterDevs.TestInsights
{
    internal class InsightsLoader
    {
        /// <summary>
        /// Ensures that the application insights dlls are loaded correctly.
        /// </summary>
        /// <remarks>
        /// This is needed because nUnit can't always find the dlls it needs, esp when run within Visual Studio.
        /// </remarks>
        public static void ForceLoadingOfAppInsightAssemblies()
        {
            Type appInsightType;
            appInsightType = typeof(DependencyTrackingTelemetryModule);
            appInsightType = typeof(PerformanceCollectorModule);
            appInsightType = typeof(ServerTelemetryChannel);
        }
    }
}
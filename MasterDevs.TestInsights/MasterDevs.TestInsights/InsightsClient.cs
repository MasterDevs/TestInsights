using Microsoft.ApplicationInsights;
using System;
using System.Diagnostics;
using System.Threading;

namespace MasterDevs.TestInsights
{
    internal static class InsightsClient
    {
        private static readonly TelemetryClient _client;

        static InsightsClient()
        {
            _client = new TelemetryClient();
            _client.Context.Session.Id = Guid.NewGuid().ToString();
        }

        internal static string InstrumentationKey
        {
            get
            {
                return _client.Context.InstrumentationKey;
            }
            set
            {
                if (!string.IsNullOrEmpty(InstrumentationKey) && InstrumentationKey != value)
                {
                    throw new Exception($"Attempting to set {InstrumentationKey} a second time");
                }

                _client.Context.InstrumentationKey = value;
            }
        }

        internal static void Flush()
        {
            Debug.WriteLine("Flushing Insights Client");
            _client.Flush();
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        internal static void Track(string name, long value)
        {
            _client.TrackMetric(name, value);
        }
    }
}
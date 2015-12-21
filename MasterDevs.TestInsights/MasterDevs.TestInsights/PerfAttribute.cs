using Microsoft.ApplicationInsights;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace MasterDevs.TestInsights
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
                AttributeTargets.Interface | AttributeTargets.Assembly,
                AllowMultiple = true)]
    public class PerfAttribute : Attribute, ITestAction
    {
        private static readonly TelemetryClient _client;
        private readonly Stopwatch _timer = new Stopwatch();

        static PerfAttribute()
        {
            _client = new TelemetryClient();
            _client.Context.Session.Id = Guid.NewGuid().ToString();
        }

        public string InstrumentationKey
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

        public ActionTargets Targets { get { return ActionTargets.Suite | ActionTargets.Test; } }

        public void AfterTest(ITest test)
        {
            PreConditionCheck(nameof(AfterTest));
            if (null != test.Method)
            {
                _timer.Stop();
                Debug.WriteLine($"Sending telemtry to AppInsights:  {test.MethodName} took {_timer.ElapsedMilliseconds}");

                _client.TrackMetric(test.MethodName, _timer.ElapsedMilliseconds);
            }
            if (test.IsSuite)
            {
                // We have to make sure that all cached messages have been sent.
                _client.Flush();
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }

        public void BeforeTest(ITest test)
        {
            PreConditionCheck(nameof(BeforeTest));
            if (test?.Method == null)
            {
                _timer.Start();
            }
        }

        private void PreConditionCheck(string methodName)
        {
            if (string.IsNullOrEmpty(InstrumentationKey))
            {
                throw new Exception($"Attempting to call {methodName} before setting {nameof(InstrumentationKey)}.  {nameof(InstrumentationKey)} needs to be set at the assembly level before {nameof(PerfAttribute)} can be applied to a test method");
            }
        }
    }
}
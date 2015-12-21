using NUnit.Framework;
using System;
using System.Diagnostics;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace MasterDevs.TestInsights
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PerfAttribute : Attribute, ITestAction
    {
        private readonly Stopwatch _timer = new Stopwatch();

        public ActionTargets Targets { get { return ActionTargets.Suite | ActionTargets.Test; } }

        public void AfterTest(TestDetails test)
        {
            if (null != test?.Method?.Name)
            {
                _timer.Stop();
                Debug.WriteLine($"Sending telemtry to AppInsights:  {test.Method.Name} took {_timer.ElapsedMilliseconds} ms");

                InsightsClient.Track(test.Method.Name, _timer.ElapsedMilliseconds);
            }
        }

        public void BeforeTest(TestDetails test)
        {
            PreConditionCheck(nameof(BeforeTest));
            if (null != test?.Method?.Name)
            {
                _timer.Start();
            }
        }

        private void PreConditionCheck(string methodName)
        {
            if (string.IsNullOrEmpty(InsightsClient.InstrumentationKey))
            {
                throw new Exception($"{nameof(PerfSetupAttribute)} must be applied to the assembly before using {nameof(PerfAttribute)}");
            }
        }
    }
}
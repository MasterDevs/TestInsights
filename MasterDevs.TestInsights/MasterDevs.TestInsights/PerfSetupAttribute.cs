using NUnit.Framework;
using System;

namespace MasterDevs.TestInsights
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PerfSetupAttribute : Attribute, ITestAction
    {
        public PerfSetupAttribute(string instrumentationKey)
        {
            if (string.IsNullOrEmpty(instrumentationKey)) throw new ArgumentException($"{nameof(instrumentationKey)} cannot be null or empty.");
            InsightsClient.InstrumentationKey = instrumentationKey;
        }

        public ActionTargets Targets { get { return ActionTargets.Suite; } }

        public void AfterTest(TestDetails testDetails)
        {
            InsightsClient.Flush();
        }

        public void BeforeTest(TestDetails testDetails)
        {
        }
    }
}
using MasterDevs.TestInsights;
using NUnit.Framework;
using System;
using System.Threading;

[assembly: PerfSetup("e80064d8-fc18-431b-9953-60d4a9c626fe")]

namespace MasterDevs.Testinsights.Tests
{
    [TestFixture]
    public class PerfAttributesTests
    {
        [Test]
        [Perf]
        public void LongTest()
        {
            // Assemble
            var before = DateTime.Now;
            Thread.Sleep(TimeSpan.FromMilliseconds(50));

            // Act
            var after = DateTime.Now;

            // Assert
            Assert.IsTrue(after > before);
        }

        [Test]
        [Perf]
        public void ShortTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        [Perf(EventName = "TestWithAGoodName")]
        public void TestWithAUselessName()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void ThisTestDoesNotReportPerformanceData()
        {
            Assert.IsFalse(1 == 0);
        }
    }
}
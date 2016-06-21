using Bands.Time;
using NUnit.Framework;
using System;
using System.Threading;

namespace Bands.Tests
{
    [TestFixture]
    public class TimedBandTests
    {
        public class TimedErrand : ITimedErrand
        {
            public TimeSpan TimeToCompleteErrand { get; set; }
        }

        [Test]
        public void TestTimedBandUsingTestMethod1_TimeToCompleteShouldBeInSleepRange()
        {
            var timedErrand = new TimedErrand();
            var timedErrandBand = new TimedBand<TimedErrand>(TestMethod1);
            timedErrandBand.Run(timedErrand);
            Assert.IsTrue(timedErrand.TimeToCompleteErrand.TotalMilliseconds > 100 && timedErrand.TimeToCompleteErrand.TotalMilliseconds < 110);
        }

        [Test]
        public void TestTimedBandUsingTestMethod2_TimeToCompleteShouldBeInSleepRange()
        {
            var timedErrand = new TimedErrand();
            var timedErrandBand = new TimedBand<TimedErrand>(TestMethod2);
            timedErrandBand.Run(timedErrand);
            Assert.IsTrue(timedErrand.TimeToCompleteErrand.TotalMilliseconds > 200 && timedErrand.TimeToCompleteErrand.TotalMilliseconds < 210);
        }

        [Test]
        public void TestTimedBandUsingNullErrandRunner_ShouldThrowArgumentException()
        {
            Action<TimedErrand> errandRunner = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedBand<TimedErrand>(errandRunner)));
        }

        [Test]
        public void TestTimedBandUsingNullInnerBand_ShouldThrowArgumentException()
        {
            Band<TimedErrand> innerBand = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedBand<TimedErrand>(innerBand)));
        }

        public void TestMethod1(TimedErrand errand)
        {
            Thread.Sleep(100);
        }

        public void TestMethod2(TimedErrand errand)
        {
            Thread.Sleep(200);
        }
    }
}

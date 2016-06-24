using Bands.Time;
using NUnit.Framework;
using System;
using System.Threading;

namespace Bands.Tests
{
    [TestFixture]
    public class TimedBandTests
    {
        public class TimedPayload : ITimedPayload
        {
            public TimeSpan TimeToCompletePayload { get; set; }
        }

        [Test]
        public void TestTimedBandUsingTestMethod1_TimeToCompleteShouldBeInSleepRange()
        {
            var timedPayload = new TimedPayload();
            var timedPayloadBand = new TimedBand<TimedPayload>(TestMethod1);
            timedPayloadBand.Enter(timedPayload);
            Assert.IsTrue(timedPayload.TimeToCompletePayload.TotalMilliseconds > 100 && timedPayload.TimeToCompletePayload.TotalMilliseconds < 110);
        }

        [Test]
        public void TestTimedBandUsingTestMethod2_TimeToCompleteShouldBeInSleepRange()
        {
            var timedPayload = new TimedPayload();
            var timedPayloadBand = new TimedBand<TimedPayload>(TestMethod2);
            timedPayloadBand.Enter(timedPayload);
            Assert.IsTrue(timedPayload.TimeToCompletePayload.TotalMilliseconds > 200 && timedPayload.TimeToCompletePayload.TotalMilliseconds < 210);
        }

        [Test]
        public void TestTimedBandUsingNullPayloadHandler_ShouldThrowArgumentException()
        {
            Action<TimedPayload> payloadHandler = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedBand<TimedPayload>(payloadHandler)));
        }

        [Test]
        public void TestTimedBandUsingNullInnerBand_ShouldThrowArgumentException()
        {
            Band<TimedPayload> innerBand = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedBand<TimedPayload>(innerBand)));
        }

        public void TestMethod1(TimedPayload payload)
        {
            Thread.Sleep(100);
        }

        public void TestMethod2(TimedPayload payload)
        {
            Thread.Sleep(200);
        }
    }
}

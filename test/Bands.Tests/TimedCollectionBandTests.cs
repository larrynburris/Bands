using Bands.Time;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bands.Tests
{
    [TestFixture]
    public class TimedCollectionBandTests
    {
        public class TimedCollectionPayload : ITimedCollectionPayload
        {
            public IEnumerable<string> Names { get; set; }

            public TimeSpan AverageTimeToCompleteEachCollectionItem { get; set; }

            public int CollectionCount { get { return Names.Count(); } }

            public TimeSpan TimeToCompleteEntireCollectionPayload { get; set; }
            
        }

        TimedCollectionPayload CollectionPayload;
        const int METHOD_ONE_SLEEP_TIME = 100;
        const int METHOD_TWO_SLEEP_TIME = 200;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            CollectionPayload = new TimedCollectionPayload { Names = new List<string> { "John", "Jack", "James" } };
        }


        [Test]
        public void TestTimedCollectionBandUsingTestMethod1_TimeToCompleteEntireCollectionShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {
            
            var timedPayloadBand = new TimedCollectionBand<TimedCollectionPayload>(TestMethod1);
            timedPayloadBand.Run(CollectionPayload);
            var minTime = CollectionPayload.CollectionCount * METHOD_ONE_SLEEP_TIME;
            var maxTime = minTime + CollectionPayload.CollectionCount * 10;
            Assert.IsTrue(CollectionPayload.TimeToCompleteEntireCollectionPayload.TotalMilliseconds >=  minTime
                       && CollectionPayload.TimeToCompleteEntireCollectionPayload.TotalMilliseconds <= maxTime);
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod1_AverageTimeToCompleteShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedPayloadBand = new TimedCollectionBand<TimedCollectionPayload>(TestMethod1);
            timedPayloadBand.Run(CollectionPayload);
            Assert.IsTrue(CollectionPayload.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds >= METHOD_ONE_SLEEP_TIME
                       && CollectionPayload.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds <= (METHOD_ONE_SLEEP_TIME+10));
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod2_TimeToCompleteEntireCollectionShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedPayloadBand = new TimedCollectionBand<TimedCollectionPayload>(TestMethod2);
            timedPayloadBand.Run(CollectionPayload);
            var minTime = CollectionPayload.CollectionCount * METHOD_TWO_SLEEP_TIME;
            var maxTime = minTime + CollectionPayload.CollectionCount * 10;
            Assert.IsTrue(CollectionPayload.TimeToCompleteEntireCollectionPayload.TotalMilliseconds >= minTime
                       && CollectionPayload.TimeToCompleteEntireCollectionPayload.TotalMilliseconds <= maxTime);
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod2_AverageTimeToCompleteShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedPayloadBand = new TimedCollectionBand<TimedCollectionPayload>(TestMethod2);
            timedPayloadBand.Run(CollectionPayload);
            Assert.IsTrue(CollectionPayload.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds >= METHOD_TWO_SLEEP_TIME
                       && CollectionPayload.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds <= (METHOD_TWO_SLEEP_TIME + 20));
        }

        [Test]
        public void TestTimedBandUsingNullPayloadHandler_ShouldThrowArgumentException()
        {
            Action<TimedCollectionPayload> payloadHandler = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedCollectionBand<TimedCollectionPayload>(payloadHandler)));
        }

        [Test]
        public void TestTimedBandUsingNullInnerBand_ShouldThrowArgumentException()
        {
            Band<TimedCollectionPayload> innerBand = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedCollectionBand<TimedCollectionPayload>(innerBand)));
        }

        public void TestMethod1(TimedCollectionPayload payload)
        {
            foreach(var name in payload.Names)
            {
                Thread.Sleep(METHOD_ONE_SLEEP_TIME);
            }
        }

        public void TestMethod2(TimedCollectionPayload payload)
        {
            foreach (var name in payload.Names)
            {
                Thread.Sleep(METHOD_TWO_SLEEP_TIME);
            }
        }
    }
}

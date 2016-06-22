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
        public class TimedCollectionErrand : ITimedCollectionErrand
        {
            public IEnumerable<string> Names { get; set; }

            public TimeSpan AverageTimeToCompleteEachCollectionItem { get; set; }

            public int CollectionCount { get { return Names.Count(); } }

            public TimeSpan TimeToCompleteEntireCollectionErrand { get; set; }
            
        }

        TimedCollectionErrand CollectionErrand;
        const int METHOD_ONE_SLEEP_TIME = 100;
        const int METHOD_TWO_SLEEP_TIME = 200;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            CollectionErrand = new TimedCollectionErrand { Names = new List<string> { "John", "Jack", "James" } };
        }


        [Test]
        public void TestTimedCollectionBandUsingTestMethod1_TimeToCompleteEntireCollectionShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {
            
            var timedErrandBand = new TimedCollectionBand<TimedCollectionErrand>(TestMethod1);
            timedErrandBand.Run(CollectionErrand);
            var minTime = CollectionErrand.CollectionCount * METHOD_ONE_SLEEP_TIME;
            var maxTime = minTime + CollectionErrand.CollectionCount * 10;
            Assert.IsTrue(CollectionErrand.TimeToCompleteEntireCollectionErrand.TotalMilliseconds >=  minTime
                       && CollectionErrand.TimeToCompleteEntireCollectionErrand.TotalMilliseconds <= maxTime);
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod1_AverageTimeToCompleteShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedErrandBand = new TimedCollectionBand<TimedCollectionErrand>(TestMethod1);
            timedErrandBand.Run(CollectionErrand);
            Assert.IsTrue(CollectionErrand.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds >= METHOD_ONE_SLEEP_TIME
                       && CollectionErrand.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds <= (METHOD_ONE_SLEEP_TIME+10));
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod2_TimeToCompleteEntireCollectionShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedErrandBand = new TimedCollectionBand<TimedCollectionErrand>(TestMethod2);
            timedErrandBand.Run(CollectionErrand);
            var minTime = CollectionErrand.CollectionCount * METHOD_TWO_SLEEP_TIME;
            var maxTime = minTime + CollectionErrand.CollectionCount * 10;
            Assert.IsTrue(CollectionErrand.TimeToCompleteEntireCollectionErrand.TotalMilliseconds >= minTime
                       && CollectionErrand.TimeToCompleteEntireCollectionErrand.TotalMilliseconds <= maxTime);
        }

        [Test]
        public void TestTimedCollectionBandUsingTestMethod2_AverageTimeToCompleteShouldBeInRangeOfSleepRangeTimesCollectionCountCount()
        {

            var timedErrandBand = new TimedCollectionBand<TimedCollectionErrand>(TestMethod2);
            timedErrandBand.Run(CollectionErrand);
            Assert.IsTrue(CollectionErrand.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds >= METHOD_TWO_SLEEP_TIME
                       && CollectionErrand.AverageTimeToCompleteEachCollectionItem.TotalMilliseconds <= (METHOD_TWO_SLEEP_TIME + 20));
        }

        [Test]
        public void TestTimedBandUsingNullErrandRunner_ShouldThrowArgumentException()
        {
            Action<TimedCollectionErrand> errandRunner = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedCollectionBand<TimedCollectionErrand>(errandRunner)));
        }

        [Test]
        public void TestTimedBandUsingNullInnerBand_ShouldThrowArgumentException()
        {
            Band<TimedCollectionErrand> innerBand = null;
            Assert.Throws(typeof(ArgumentException), (() => new TimedCollectionBand<TimedCollectionErrand>(innerBand)));
        }

        public void TestMethod1(TimedCollectionErrand errand)
        {
            foreach(var name in errand.Names)
            {
                Thread.Sleep(METHOD_ONE_SLEEP_TIME);
            }
        }

        public void TestMethod2(TimedCollectionErrand errand)
        {
            foreach (var name in errand.Names)
            {
                Thread.Sleep(METHOD_TWO_SLEEP_TIME);
            }
        }
    }
}

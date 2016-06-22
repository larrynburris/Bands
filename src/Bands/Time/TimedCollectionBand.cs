using System;
using System.Diagnostics;

namespace Bands.Time
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with a stopwatch and extracting timespan 
    /// required to complete execution of inner functionality. In addition, this band will extract
    /// the average execution time per item in the collection.
    /// </summary>
    /// <typeparam name="TTimedCollectionErrand">Type of timed collection errand</typeparam>
    public class TimedCollectionBand<TTimedCollectionErrand> : Band<TTimedCollectionErrand> 
        where TTimedCollectionErrand : ITimedCollectionErrand
    {
        public TimedCollectionBand(IBand<TTimedCollectionErrand> errand) : base(errand) { }

        public TimedCollectionBand(Action<TTimedCollectionErrand> errandRunner) : base(errandRunner) { }

        /// <summary>
        /// Determine timespan required to execute inner bands and/or wrapped functionality and determine average
        /// time per collection item.
        /// </summary>
        /// <param name="errand">Type of timed collection errand</param>
        public new void Run(TTimedCollectionErrand errand)
        {
            var sw = new Stopwatch();
            sw.Start();
            InnerBand.Run(errand);
            sw.Stop();
            errand.TimeToCompleteEntireCollectionErrand = sw.Elapsed;
            errand.AverageTimeToCompleteEachCollectionItem = new TimeSpan(0, 0, 0, 0, (int)(sw.ElapsedMilliseconds / errand.CollectionCount));
        }
    }
}

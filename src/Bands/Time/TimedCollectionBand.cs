using System;
using System.Diagnostics;

namespace Bands.Time
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with a stopwatch and extracting timespan 
    /// required to complete execution of inner functionality. In addition, this band will extract
    /// the average execution time per item in the collection.
    /// </summary>
    /// <typeparam name="TTimedCollectionPayload">Type of timed collection payload</typeparam>
    public class TimedCollectionBand<TTimedCollectionPayload> : Band<TTimedCollectionPayload> 
        where TTimedCollectionPayload : ITimedCollectionPayload
    {
        public TimedCollectionBand(IBand<TTimedCollectionPayload> payload) : base(payload) { }

        public TimedCollectionBand(Action<TTimedCollectionPayload> payloadHandler) : base(payloadHandler) { }

        /// <summary>
        /// Determine timespan required to execute inner bands and/or wrapped functionality and determine average
        /// time per collection item.
        /// </summary>
        /// <param name="payload">A timed collection payload</param>
        public override void Run(TTimedCollectionPayload payload)
        {
            var sw = new Stopwatch();
            sw.Start();
            InnerBand.Run(payload);
            sw.Stop();
            payload.TimeToCompleteEntireCollectionPayload = sw.Elapsed;
            payload.AverageTimeToCompleteEachCollectionItem = new TimeSpan(0, 0, 0, 0, (int)(sw.ElapsedMilliseconds / payload.CollectionCount));
        }
    }
}

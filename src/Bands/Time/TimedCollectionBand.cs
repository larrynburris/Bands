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
        Stopwatch _stopwatch;

        public TimedCollectionBand(IBand<TTimedCollectionPayload> payload) : base(payload) { }

        public TimedCollectionBand(Action<TTimedCollectionPayload> payloadHandler) : base(payloadHandler) { }

        /// <summary>
        /// Starts stopwatch prior to executing inner bands and/or wrapped funtionality
        /// </summary>
        /// <param name="payload">A payload of type <typeparamref name="ITimedCollectionPayload"/></param>
        public override void OnEnter(TTimedCollectionPayload payload)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops stopwatch on completing execution of inner bands and/or wrapped funtionality
        /// and calculates total timespan and average time span per collection item
        /// </summary>
        /// <param name="payload">A payload of type <typeparamref name="ITimedCollectionPayload"/></param>
        public override void OnExit(TTimedCollectionPayload payload)
        {
            _stopwatch.Stop();
            payload.TimeToCompleteEntireCollectionPayload = _stopwatch.Elapsed;
            var averagePerItem = GetAverageMsPerCollectionItem(_stopwatch.ElapsedMilliseconds, payload.CollectionCount);
            payload.AverageTimeToCompleteEachCollectionItem = new TimeSpan(0,0,0,0,averagePerItem);
        }

        /// <summary>
        /// Calculates average milliseconds per collection item
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds for entire collection</param>
        /// <param name="count">Number of items in collection</param>
        /// <returns></returns>
        private int GetAverageMsPerCollectionItem(long milliseconds, int count)
        {
            return Convert.ToInt32(Math.Round(((double)milliseconds / count), MidpointRounding.AwayFromZero));
        }
    }
}

using System;
using System.Diagnostics;

namespace Bands.Time
{
    /// <summary>
    /// Helper class for wrapper some inner functionality with a stopwatch and extracting timespan 
    /// required to complete execution of inner functionality.
    /// </summary>
    /// <typeparam name="TPayload">Type of timed payload</typeparam>
    public class TimedBand<TPayload> : Band<TPayload> where TPayload : ITimedPayload
    {
        public TimedBand(IBand<TPayload> payload) : base(payload) { }

        public TimedBand(Action<TPayload> payloadHandler) : base(payloadHandler) { }

        /// <summary>
        /// Determine timespan required to execute inner bands and/or wrapped functionality.
        /// </summary>
        /// <param name="payload">Type of timed payload</param>
        public new void Run(TPayload payload)
        {
            var sw = new Stopwatch();
            sw.Start();
            InnerBand.Run(payload);
            sw.Stop();
            payload.TimeToCompletePayload = sw.Elapsed;
        }
    }
}

using System;
using System.Diagnostics;

namespace Bands.Time
{
    /// <summary>
    /// Helper class for wrapper some inner functionality with a stopwatch and extracting timespan 
    /// required to complete execution of inner functionality.
    /// </summary>
    /// <typeparam name="TPayload">Type of timed payload.</typeparam>
    public class TimedBand<TPayload> : Band<TPayload> where TPayload : ITimedPayload
    {
        Stopwatch _stopwatch;

        public TimedBand(IBand<TPayload> payload) : base(payload) { }

        public TimedBand(Action<TPayload> payloadHandler) : base(payloadHandler) { }

        public override void OnEnter(TPayload payload)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public override void OnExit(TPayload payload)
        {
            _stopwatch.Stop();
            payload.TimeToCompletePayload = _stopwatch.Elapsed;
        }
    }
}

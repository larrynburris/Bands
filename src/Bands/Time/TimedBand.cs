using System;
using System.Diagnostics;

namespace Bands.Time
{
    /// <summary>
    /// Helper class for wrapper some inner functionality with a stopwatch and extracting timespan required to complete execution of inner functionality.
    /// </summary>
    /// <typeparam name="TErrand">Type of timed errand</typeparam>
    public class TimedBand<TErrand> : Band<TErrand> where TErrand : ITimedErrand
    {
        public TimedBand(IBand<TErrand> errand) : base(errand) { }

        public TimedBand(Action<TErrand> errandRunner) : base(errandRunner) { }

        public new void Run(TErrand errand)
        {
            var sw = new Stopwatch();
            sw.Start();
            InnerBand.Run(errand);
            sw.Stop();
            errand.TimeToCompleteErrand = sw.Elapsed;
        }
    }
}

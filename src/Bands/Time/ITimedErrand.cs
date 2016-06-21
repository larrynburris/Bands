using System;

namespace Bands.Time
{
    /// <summary>
    /// A serializable function call which will be timed. An errand contains the appropriate information for executing some functionality.
    /// </summary>
    public interface ITimedErrand : IErrand
    {
        TimeSpan TimeToCompleteErrand { get; set; }
    }
}

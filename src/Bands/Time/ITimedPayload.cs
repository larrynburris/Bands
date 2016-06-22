using System;

namespace Bands.Time
{
    /// <summary>
    /// An object that contains the appropriate information for executing some functionality.
    /// </summary>
    public interface ITimedPayload : IPayload
    {
        TimeSpan TimeToCompletePayload { get; set; }
    }
}

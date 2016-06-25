using System;

namespace Bands.Time
{
    /// <summary>
    /// A serializable function call containing a collection which will be timed. In addition to the total time, the
    /// average time of completion per list item will be calculated. A payload contains the appropriate information 
    /// for executing some functionality.
    /// </summary>
    public interface ITimedCollectionPayload : IPayload
    {
        int CollectionCount { get; }
        TimeSpan TimeToCompleteEntireCollectionPayload { get; set; }
        TimeSpan AverageTimeToCompleteEachCollectionItem { get; set; }
    }
}

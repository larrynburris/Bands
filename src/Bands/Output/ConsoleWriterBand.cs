using System;

namespace Bands.Output
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with a console writer. The IWritable.ToString()
    /// result will be passed to the output before and after inner functionality is executed.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class ConsoleWriterBand<TPayload> : Band<TPayload> where TPayload : WritablePayload
    {
        public ConsoleWriterBand(Action<TPayload> action) : base(action) { }

        public ConsoleWriterBand(IBand<TPayload> innerBand) : base(innerBand) { }

        /// <summary>
        /// Print <paramref name="payload"/>.ToString() to console prior to executing inner bands
        /// and/or wrapped functionality
        /// </summary>
        /// <param name="payload">A WritablePayload</param>
        public override void OnEnter(TPayload payload)
        {
            Console.WriteLine(payload.ToString());
        }

        /// <summary>
        /// Print <paramref name="payload"/>.ToString() to console after executing inner bands
        /// and/or wrapped functionality
        /// </summary>
        /// <param name="payload"></param>
        public override void OnExit(TPayload payload)
        {
            Console.WriteLine(payload.ToString());
        }
    }
}

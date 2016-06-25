using System;
using System.Diagnostics;

namespace Bands.Output.TraceWriter
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with tracing
    /// functionality. The <see cref="IWritable.ToString()"/> result will be
    /// passed to the output before and after inner functionality is executed.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class TraceWriterBand<TPayload> : Band<TPayload> 
        where TPayload : WritablePayload
    {

        /// <summary>
        /// Initialize <see cref="TraceWriterBand{TPayload}"/> around some 
        /// functionality. Parameter <paramref name="action"/> will be passed
        /// to special band <see cref="InnerMostBand{T}"/> and initialized as
        /// the PayloadHandler.
        /// </summary>
        /// <param name="action">
        /// Any <see cref="Action{TPayload}"/> where <see cref="TPayload"/>
        /// inherits from <see cref="WritablePayload"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// ArgumentException is thrown if <paramref name="action"/> is null.
        /// </exception>
        public TraceWriterBand(Action<TPayload> action) : base(action) { }

        /// <summary> Initialize band around some functionality.
        /// Initialize <see cref="TraceWriterBand{TPayload}"/> with <paramref name="innerBand"/>
        /// set as InnerBand.
        /// </summary>
        /// <param name="innerBand">
        /// Any <see cref="IBand{TPayload}"/> for further wrapping some inner functionality.
        /// </param>
        /// <exception cref="ArgumentException">
        /// ArgumentException is thrown if <paramref name="innerBand"/> is null.
        /// </exception>
        public TraceWriterBand(IBand<TPayload> innerBand) : base(innerBand) { }

        /// <summary>
        /// Traces <paramref name="payload"/>.ToString() on entering 
        /// <see cref="TraceWriterBand{TPayload}"/>.
        /// </summary>
        /// <param name="payload">
        /// A payload derived from <see cref="WritablePayload"/>.
        /// </param>
        public override void OnEnter(TPayload payload)
        {
            Trace.TraceInformation(payload.ToString());
        }

        /// <summary>
        /// Traces <paramref name="payload"/>.ToString() on exiting 
        /// <see cref="TraceWriterBand{TPayload}"/>.
        /// </summary>
        /// <param name="payload">
        /// A payload derived from <see cref="WritablePayload"/>.
        /// </param>
        public override void OnExit(TPayload payload)
        {
            Trace.TraceInformation(payload.ToString());
        }
    }
}

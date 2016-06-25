using Bands.Logging;
using System;

namespace Bands.Output.LogWriter
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with logging
    /// functionality. The <see cref="IWritable.ToString()"/> result will be
    /// passed to the output before and after inner functionality is executed.
    /// <para/>
    /// Note: <see cref="LoggerBand{TPayload}"/> is using LibLog to dynamically
    /// determine the appropriate logging framework.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class LoggerBand<TPayload> : Band<TPayload> 
        where TPayload : WritablePayload
    {
        ILog Logger;

        /// <summary>
        /// Initialize <see cref="LoggerBand{TPayload}"/> around some 
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
        public LoggerBand(Action<TPayload> action) : base(action)
        {
            Logger = LogProvider.GetCurrentClassLogger();
        }

        /// <summary> Initialize band around some functionality.
        /// Initialize <see cref="LoggerBand{TPayload}"/> with <paramref name="innerBand"/>
        /// set as InnerBand.
        /// </summary>
        /// <param name="innerBand">
        /// Any <see cref="IBand{TPayload}"/> for further wrapping some inner functionality.
        /// </param>
        /// <exception cref="ArgumentException">
        /// ArgumentException is thrown if <paramref name="innerBand"/> is null.
        /// </exception>
        public LoggerBand(IBand<TPayload> innerBand) : base(innerBand)
        {
            Logger = LogProvider.GetCurrentClassLogger();
        }

        /// <summary>
        /// Logs <paramref name="payload"/>.ToString() on entering 
        /// <see cref="LoggerBand{TPayload}"/>.
        /// </summary>
        /// <param name="payload">
        /// A payload derived from <see cref="WritablePayload"/>.
        /// </param>
        public override void OnEnter(TPayload payload)
        {
            Logger.Info(payload.ToString());
        }

        /// <summary>
        /// Logs <paramref name="payload"/>.ToString() on exiting 
        /// <see cref="LoggerBand{TPayload}"/>.
        /// </summary>
        /// <param name="payload">
        /// A payload derived from <see cref="WritablePayload"/>.
        /// </param>
        public override void OnExit(TPayload payload)
        {
            Logger.Info(payload.ToString());
        }
    }
}

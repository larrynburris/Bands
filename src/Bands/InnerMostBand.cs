using System;

namespace Bands
{
    /// <summary>
    /// A band used to provide an interface to the wrapped functionality.
    /// </summary>
    /// <typeparam name="T">Type of payload</typeparam>
    public class InnerMostBand<T> : IBand<T> where T : IPayload
    {
        /// <summary>
        /// A method accepting the payload <paramref name="T"/> that represents the inner wrapped functionality.
        /// </summary>
        private Action<T> payloadHandler;


        /// <summary>
        /// Initialize with wrapped functionality
        /// </summary>
        /// <param name="action"></param>
        public InnerMostBand(Action<T> action)
        {
            payloadHandler = action;
        }

        /// <summary>
        /// Run the wrapped functionality.
        /// </summary>
        /// <param name="payload">A payload</param>
        public void Enter(T payload)
        {
            payloadHandler(payload);
        }
    }
}

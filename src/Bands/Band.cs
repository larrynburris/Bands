using System;

namespace Bands
{
    /// <summary>
    /// Helper class for wrapping a band of functionality around some inner functionality.
    /// </summary>
    /// <typeparam name="T">The type of payload</typeparam>
    public abstract class Band<T> : IBand<T> where T : IPayload
    {
        protected IBand<T> InnerBand;

        /// <summary>
        /// Initialize band around another band.
        /// </summary>
        /// <param name="innerBand">Any band for further wrapping some inner functionality.</param>
        /// <exception cref="ArgumentException">ArgumentException is thrown if <paramref name="innerBand"/> is null</exception>
        public Band(IBand<T> innerBand)
        {
            if (innerBand == null)
                throw new ArgumentException("Supplied parameter innerBand cannot be bull.");

            InnerBand = innerBand;
        }

        /// <summary>
        /// Initialize band around some functionality.
        /// </summary>
        /// <param name="payloadHandler">Any function accepting the specified generic constraint as a parameter.</param>
        /// <exception cref="ArgumentException">ArgumentException is thrown if <paramref name="payloadHandler"/> is null</exception>
        public Band(Action<T> payloadHandler)
        {
            if (payloadHandler == null)
                throw new ArgumentException("Supplied parameter payload handler cannot be null.");

            InnerBand = new InnerMostBand<T>(payloadHandler);
        }
        
        /// <summary>
        /// Run the next inner band and/or the wrapped functionality
        /// </summary>
        /// <param name="payload">A playload</param>
        public void Run(T payload)
        {
            if (InnerBand == null)
                throw new ApplicationException("Band has no inner band.");

            InnerBand.Run(payload);
        }
    }
}

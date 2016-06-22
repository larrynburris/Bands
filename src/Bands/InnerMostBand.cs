using System;

namespace Bands
{
    /// <summary>
    /// A band used to provide an interface to the wrapped functionality.
    /// </summary>
    /// <typeparam name="T">Type of errand</typeparam>
    public class InnerMostBand<T> : IBand<T> where T : IErrand
    {
        /// <summary>
        /// A method accepting the errand <paramref name="T"/> that represents the inner wrapped functionality.
        /// </summary>
        private Action<T> errandRunner;


        /// <summary>
        /// Initialize with wrapped functionality
        /// </summary>
        /// <param name="action"></param>
        public InnerMostBand(Action<T> action)
        {
            errandRunner = action;
        }

        /// <summary>
        /// Run the wrapped functionality.
        /// </summary>
        /// <param name="errand"></param>
        public void Run(T errand)
        {
            errandRunner(errand);
        }
    }
}

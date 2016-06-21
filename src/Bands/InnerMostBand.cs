using System;

namespace Bands
{
    public class InnerMostBand<T> : IBand<T> where T : IErrand
    {
        private Action<T> errandRunner;

        public InnerMostBand(Action<T> action)
        {
            errandRunner = action;
        }

        public void Run(T errand)
        {
            errandRunner(errand);
        }
    }
}

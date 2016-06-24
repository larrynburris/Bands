using System;

namespace Bands.GettingStarted
{
    public class IncrementableBand<TPayload> : Band<TPayload> where TPayload : IIncrementablePayload
    {
        public IncrementableBand(Action<TPayload> action) : base(action) { }

        public IncrementableBand(IBand<TPayload> innerBand) : base(innerBand) { }

        public override void Run(TPayload payload)
        {
            Console.WriteLine("Calling payload.Increment()");
            payload.Increment();

            InnerBand.Run(payload);

            Console.WriteLine("Calling payload.Decrement()");
            payload.Decrement();
        }
    }
}

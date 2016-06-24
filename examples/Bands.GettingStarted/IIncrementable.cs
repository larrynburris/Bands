namespace Bands.GettingStarted
{
    public interface IIncrementable
    {
        int Counter { get; }

        void Increment();
        void Decrement();
    }
}

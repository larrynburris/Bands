namespace Bands
{
    /// <summary>
    /// Defines a band of functionality that is wrapped around some inner functionality.
    /// </summary>
    /// <typeparam name="T">The type of errand</typeparam>
    public interface IBand<T> where T : IErrand
    {
        void Run(T errand);
    }
}

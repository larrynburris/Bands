namespace Bands
{
    /// <summary>
    /// Defines a band of functionality that is wrapped around some inner functionality.
    /// </summary>
    /// <typeparam name="T">The type of payload</typeparam>
    public interface IBand<T> where T : IPayload
    {
        void Run(T payload);
    }
}

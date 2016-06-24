namespace Bands.Output
{
    /// <summary>
    /// A writable object that contains the appropriate information for executing some functionality.
    /// </summary>
    public abstract class WritablePayload : IWritable, IPayload
    {
        public abstract override string ToString();
    }
}

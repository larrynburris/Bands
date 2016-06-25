namespace Bands.Output
{
    /// <summary>
    /// Defines an objects ability to be converted to a string and written to some stream.
    /// </summary>
    public interface IWritable
    {
        string ToString();
    }
}

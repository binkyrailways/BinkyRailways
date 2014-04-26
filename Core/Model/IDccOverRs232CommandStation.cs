namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// DCC command station that generates it's protocol over an RS232 connection.
    /// </summary>
    public interface IDccOverRs232CommandStation : ISerialPortCommandStation 
    {
    }
}

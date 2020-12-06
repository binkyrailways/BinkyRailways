namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Command station that uses a serial port.
    /// </summary>
    public interface ISerialPortCommandStation  : ICommandStation 
    {
        /// <summary>
        /// Name of COM port used to communicate with the locobuffer.
        /// </summary>
        string ComPortName { get; set; }
    }
}

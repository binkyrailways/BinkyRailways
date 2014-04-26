namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Turntable or fiddle yard
    /// </summary>
    public interface ITurnTable : IJunction, IAddressEntity
    {
        /// <summary>
        /// Address of first position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress1 { get; set; }

        /// <summary>
        /// Address of second position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress2 { get; set; }

        /// <summary>
        /// Address of third position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress3 { get; set; }

        /// <summary>
        /// Address of fourth position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress4 { get; set; }

        /// <summary>
        /// Address of fifth position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress5 { get; set; }

        /// <summary>
        /// Address of sixed position bit.
        /// This is an output signal.
        /// </summary>
        Address PositionAddress6 { get; set; }

        /// <summary>
        /// If set, the straight/off commands used for position addresses are inverted.
        /// </summary>
        bool InvertPositions { get; set; }

        /// <summary>
        /// Address of the line used to indicate a "write address".
        /// This is an output signal.
        /// </summary>
        Address WriteAddress { get; set; }

        /// <summary>
        /// If set, the straight/off command used for "write address" line is inverted.
        /// </summary>
        bool InvertWrite { get; set; }

        /// <summary>
        /// Address of the line used to indicate a "change of position in progress".
        /// This is an input signal.
        /// </summary>
        Address BusyAddress { get; set; }

        /// <summary>
        /// If set, the input level used for "busy" line is inverted.
        /// </summary>
        bool InvertBusy { get; set; }

        /// <summary>
        /// First position number. Typically 1.
        /// </summary>
        int FirstPosition { get; set; }

        /// <summary>
        /// Last position number. Typically 63.
        /// </summary>
        int LastPosition { get; set; }

        /// <summary>
        /// Position number used to initialize the turntable with?
        /// </summary>
        int InitialPosition { get; set; }
    }
}

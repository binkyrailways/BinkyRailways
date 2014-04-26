namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Device that outputs some signal on the railway.
    /// </summary>
    public interface IBlockSignal : ISignal
    {
        /// <summary>
        /// First address.
        /// This is an output signal.
        /// </summary>
        Address Address1 { get; set; }

        /// <summary>
        /// Second address.
        /// This is an output signal.
        /// </summary>
        Address Address2 { get; set; }

        /// <summary>
        /// Third address.
        /// This is an output signal.
        /// </summary>
        Address Address3 { get; set; }

        /// <summary>
        /// Fourth address.
        /// This is an output signal.
        /// </summary>
        Address Address4 { get; set; }

        /// <summary>
        /// Is the Red color available?
        /// </summary>
        bool IsRedAvailable { get; }

        /// <summary>
        /// Bit pattern used for color Red.
        /// Set to <see cref="BlockSignalPatterns.Disabled"/> when Red is not supported.
        /// </summary>
        int RedPattern { get; set; }

        /// <summary>
        /// Is the Green color available?
        /// </summary>
        bool IsGreenAvailable { get; }

        /// <summary>
        /// Bit pattern used for color Green.
        /// Set to <see cref="BlockSignalPatterns.Disabled"/> when Green is not supported.
        /// </summary>
        int GreenPattern { get; set; }

        /// <summary>
        /// Is the Yellow color available?
        /// </summary>
        bool IsYellowAvailable { get; }

        /// <summary>
        /// Bit pattern used for color Yellow.
        /// Set to <see cref="BlockSignalPatterns.Disabled"/> when Yellow is not supported.
        /// </summary>
        int YellowPattern { get; set; }

        /// <summary>
        /// Is the White color available?
        /// </summary>
        bool IsWhiteAvailable { get; }

        /// <summary>
        /// Bit pattern used for color White.
        /// Set to <see cref="BlockSignalPatterns.Disabled"/> when White is not supported.
        /// </summary>
        int WhitePattern { get; set; }

        /// <summary>
        /// The block this signal protects.
        /// </summary>
        IBlock Block { get; set; }

        /// <summary>
        /// Side of the block where the signal is located.
        /// </summary>
        BlockSide Position { get; set; }

        /// <summary>
        /// Type of signal
        /// </summary>
        BlockSignalType Type { get; set; }
    }
}

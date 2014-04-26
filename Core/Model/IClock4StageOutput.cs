namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Send clock signal in 2 bits to the track.
    /// </summary>
    public interface IClock4StageOutput : IAddressEntity, IOutput
    {
        /// <summary>
        /// Address of first clock bit.
        /// This is an output signal.
        /// </summary>
        Address Address1 { get; set; }

        /// <summary>
        /// Address of second clock bit.
        /// This is an output signal.
        /// </summary>
        Address Address2 { get; set; }

        /// <summary>
        /// Bit pattern used for "morning".
        /// </summary>
        int MorningPattern { get; set; }

        /// <summary>
        /// Bit pattern used for "afternoon".
        /// </summary>
        int AfternoonPattern { get; set; }

        /// <summary>
        /// Bit pattern used for "evening".
        /// </summary>
        int EveningPattern { get; set; }

        /// <summary>
        /// Bit pattern used for "night".
        /// </summary>
        int NightPattern { get; set; }
    }
}

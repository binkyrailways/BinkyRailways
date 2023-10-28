namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Standard two-way left/right switch
    /// </summary>
    public interface ISwitch : IJunction, IAddressEntity
    {
        /// <summary>
        /// Does this switch send a feedback when switched?
        /// </summary>
        bool HasFeedback { get; set; }

        /// <summary>
        /// Address of the feedback unit of the entity
        /// </summary>
        Address FeedbackAddress { get; set; }

        /// <summary>
        /// Time (in ms) it takes for the switch to move from one direction to the other?
        /// This property is only used when <see cref="HasFeedback"/> is false.
        /// </summary>
        int SwitchDuration { get; set; }

        /// <summary>
        /// If set, the straight/off commands are inverted.
        /// </summary>
        bool Invert { get; set; }

        /// <summary>
        /// If there is a different feedback address and this is set, the straight/off feedback states are inverted.
        /// </summary>
        bool InvertFeedback { get; set; }

        /// <summary>
        /// At which direction should the switch be initialized?
        /// </summary>
        SwitchDirection InitialDirection { get; set; }
    }
}

using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single standard switch.
    /// </summary>
    public interface ISwitchState  : IJunctionState
    {
        /// <summary>
        /// Address of the entity
        /// </summary>
        Address Address { get; }

        /// <summary>
        /// Address of the feedback line of the entity
        /// </summary>
        Address FeedbackAddress { get; }

        /// <summary>
        /// Does this switch send a feedback when switched?
        /// </summary>
        bool HasFeedback { get; }

        /// <summary>
        /// Time (in ms) it takes for the switch to move from one direction to the other?
        /// This property is only used when <see cref="HasFeedback"/> is false.
        /// </summary>
        int SwitchDuration { get; }

        /// <summary>
        /// If set, the straight/off commands are inverted.
        /// </summary>
        bool Invert { get; }

        /// <summary>
        /// If set, the straight/off feedback states are inverted.
        /// </summary>
        bool InvertFeedback { get; }

        /// <summary>
        /// Direction of the switch.
        /// </summary>
        IStateProperty<SwitchDirection> Direction { get; }
    }
}

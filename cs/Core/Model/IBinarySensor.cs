namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Device that signals an event on the railway with a state of "on" or "off".
    /// </summary>
    public interface IBinarySensor : ISensor, IActionTriggerSource
    {
        /// <summary>
        /// Trigger fired when the sensor becomes active.
        /// </summary>
        IActionTrigger ActivateTrigger { get; }

        /// <summary>
        /// Trigger fired when the sensor becomes in-active.
        /// </summary>
        IActionTrigger DeActivateTrigger { get; }
    }
}

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of sensors.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface ISensorSet : IEntitySet<ISensor>
    {
        /// <summary>
        /// Add a new binary sensor
        /// </summary>
        IBinarySensor AddNewBinarySensor();
    }
}

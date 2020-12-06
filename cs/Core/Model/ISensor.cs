namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Device that signals some event on the railway.
    /// </summary>
    public interface ISensor : IAddressEntity, IPositionedEntity, IModuleEntity
    {
        /// <summary>
        /// The block that this sensor belongs to.
        /// When set, this connection is used in the loc-to-block assignment process.
        /// </summary>
        IBlock Block { get; set; }

        /// <summary>
        /// Shape used to visualize this sensor
        /// </summary>
        Shapes Shape { get; set; }
    }
}

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of outputs.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IOutputSet : IEntitySet<IOutput>
    {
        /// <summary>
        /// Add a new binary output
        /// </summary>
        IBinaryOutput AddNewBinaryOutput();

        /// <summary>
        /// Add a new 4-stage clock output
        /// </summary>
        IClock4StageOutput AddNewClock4StageOutput();
    }
}

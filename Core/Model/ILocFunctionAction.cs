namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Action that changes the function state of a loc.
    /// </summary>
    public interface ILocFunctionAction : ILocAction
    {
        /// <summary>
        /// The function involved in the action.
        /// </summary>
        LocFunction Function { get; set; }

        /// <summary>
        /// What to do with the function
        /// </summary>
        LocFunctionCommand Command { get; set; }
    }
}

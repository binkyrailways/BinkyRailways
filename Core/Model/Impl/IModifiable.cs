namespace BinkyRailways.Core.Model.Impl
{
    internal interface IModifiable
    {
        /// <summary>
        /// Called when a property on this is modified
        /// </summary>
        void OnModified();
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for entities contained in a railway.
    /// </summary>
    public abstract class RailwayEntity : Entity
    {
        /// <summary>
        /// Railway which contains this entity
        /// </summary>
        internal Railway Railway { get; set; }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected override Railway Root
        {
            get { return Railway; }
        }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal override void OnModified()
        {
            if (Railway != null)
            {
                Railway.OnModified();
            }
        }
    }
}

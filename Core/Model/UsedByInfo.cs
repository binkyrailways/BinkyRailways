namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Single usage info.
    /// </summary>
    public sealed class UsedByInfo
    {
        private readonly IEntity usedBy;
        private readonly string message;

        /// <summary>
        /// Default ctor
        /// </summary>
        public UsedByInfo(IEntity usedBy, string message)
        {
            this.usedBy = usedBy;
            this.message = message;
        }

        /// <summary>
        /// Human readable message explaining the used by relation
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Entity that is using the entity we're asking usage info from.
        /// </summary>
        public IEntity UsedBy
        {
            get { return usedBy; }
        }
    }
}

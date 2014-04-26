namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// FeedbackManager object (id=26)
    /// </summary>
    public class FeedbackManager : Object
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public FeedbackManager(Client client)
            : base(client, IdFeedbackManager)
        {
        }
    }
}

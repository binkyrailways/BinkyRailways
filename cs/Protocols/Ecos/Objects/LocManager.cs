namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// LokManager object (id=10)
    /// </summary>
    public class LocManager : Object
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocManager(Client client)
            : base(client, IdLocManager)
        {
        }
    }
}

using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// ECoS loc object
    /// </summary>
    internal class Loc : Protocols.Ecos.Objects.Loc
    {
        private readonly ILocState locState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Loc(Client client, int id, ILocState locState)
            : base(client, id)
        {
            this.locState = locState;
        }
    }
}

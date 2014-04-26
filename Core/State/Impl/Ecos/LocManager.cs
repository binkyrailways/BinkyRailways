using System.Collections.Concurrent;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Ecos;
using RawLoc = BinkyRailways.Protocols.Ecos.Objects.Loc;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    internal class LocManager : Protocols.Ecos.Objects.LocManager
    {
        private readonly IRailwayState railwayState;
        private readonly ConcurrentDictionary<ILocState, Loc> locIds = new ConcurrentDictionary<ILocState, Loc>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocManager(Client client, IRailwayState railwayState)
            : base(client)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal bool TryGetLoc(ILocState locState, out Loc loc)
        {
            return locIds.TryGetValue(locState, out loc);
        }

        /// <summary>
        /// QueryObjects reply.
        /// </summary>
        protected override void OnQueryObjects(Reply reply)
        {
            foreach (var row in reply.Rows)
            {
                var raw = new RawLoc(Client, row.Id);
                var protocol = raw.GetProtocol();
                AddressType addressType;

                if (!EcosUtility.TryGetAddressType(protocol, out addressType))
                    continue;

                var addr = raw.GetAddress();
                int addressNr;
                if (!int.TryParse(addr, out addressNr))
                    continue;

                var name = raw.GetName();
                // Try get by address
                var loc = railwayState.LocStates.FirstOrDefault(x => (x.Address.Value == addressNr) && (x.Address.Type == addressType));
                // Try get by name
                loc = loc ?? railwayState.LocStates.FirstOrDefault(x => x.Description == name);

                if (loc != null)
                {
                    // We found a match
                    locIds[loc] = new Loc(Client, row.Id, loc);
                }
            }
        }
    }
}

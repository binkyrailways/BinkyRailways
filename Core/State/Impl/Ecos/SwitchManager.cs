using System;
using System.Collections.Concurrent;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Ecos;
using RawSwitch = BinkyRailways.Protocols.Ecos.Objects.Switch;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    internal class SwitchManager : Protocols.Ecos.Objects.SwitchManager 
    {
        private readonly IRailwayState railwayState;
        private readonly ConcurrentDictionary<IJunctionState, Switch> junctionIds = new ConcurrentDictionary<IJunctionState, Switch>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchManager(Client client, IRailwayState railwayState)
            : base(client)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Gets advanced info for the given junction
        /// </summary>
        internal bool TryGetJunction(IJunctionState junctionState, out Switch @switch)
        {
            return junctionIds.TryGetValue(junctionState, out @switch);
        }

        /// <summary>
        /// QueryObjects reply.
        /// </summary>
        protected override void OnQueryObjects(Reply reply)
        {
            foreach (var row in reply.Rows)
            {
                var raw = new RawSwitch(Client, row.Id);
                var protocol = raw.GetProtocol();
                AddressType addressType;

                if (!EcosUtility.TryGetAddressType(protocol, out addressType))
                    continue;

                var addr = raw.GetAddress();
                int addressNr;
                if (!int.TryParse(addr, out addressNr))
                    continue;

                var junction = railwayState.JunctionStates.FirstOrDefault(x => Matches(x, addressType, addressNr));
                if (junction != null)
                {
                    var sw = new Switch(Client, row.Id, (ISwitchState) junction);
                    junctionIds[junction] = sw;
                    sw.RequestView();
                }
            }
        }

        private static bool Matches(IJunctionState junctionState, AddressType addressType, int addressNr)
        {
            var @switchState = junctionState as ISwitchState;
            if (@switchState != null)
            {
                var addr = @switchState.Address;
                return (addr.Type == addressType) && (addr.ValueAsInt == addressNr);
            }
            return false;
        }
    }
}

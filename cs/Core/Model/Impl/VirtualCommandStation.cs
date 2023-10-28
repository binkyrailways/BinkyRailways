using System.Collections.Generic;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Virtual mode command station
    /// </summary>
    public sealed class VirtualCommandStation : CommandStation, IVirtualCommandStation
    {
        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity)
        {
            return GetSupportedAddressTypes();
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes()
        {
            yield return AddressType.LocoNet;
            yield return AddressType.Dcc;
            yield return AddressType.Motorola;
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}

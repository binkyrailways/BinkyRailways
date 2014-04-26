using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// Utility functions
    /// </summary>
    public static class EcosUtility
    {
        /// <summary>
        /// Convert Ecos protocol to address type.
        /// </summary>
        public static bool TryGetAddressType(string protocol, out AddressType addressType)
        {
            switch (protocol)
            {
                case "DCC":
                case "DCC14":
                case "DCC28":
                case "DCC128":
                    addressType = AddressType.Dcc;
                    break;
                case "MM14":
                    addressType = AddressType.Motorola;
                    break;
                case "MFX":
                    addressType = AddressType.Mfx;
                    break;
                default:
                    addressType = AddressType.Dcc;
                    return false;
            }
            return true;
        }
    }
}

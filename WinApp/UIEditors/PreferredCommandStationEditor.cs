using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for preferred command stations
    /// </summary>
    internal abstract class PreferredCommandStationEditor : CommandStationEditor
    {
        private readonly AddressType addressType;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected PreferredCommandStationEditor(AddressType addressType)
        {
            this.addressType = addressType;
        }

        /// <summary>
        /// Select the command stations to choose from
        /// </summary>
        protected override IEnumerable<ICommandStation> SelectCommandStations(IRailway railway)
        {
            return base.SelectCommandStations(railway).Where(x => x.GetSupportedAddressTypes().Contains(addressType));
        }
    }

    internal sealed class PreferredDccCommandStationEditor : PreferredCommandStationEditor
    {
        public PreferredDccCommandStationEditor() : base(AddressType.Dcc) {}
    }

    internal sealed class PreferredLocoNetCommandStationEditor : PreferredCommandStationEditor
    {
        public PreferredLocoNetCommandStationEditor() : base(AddressType.LocoNet) { }
    }

    internal sealed class PreferredMotorolaCommandStationEditor : PreferredCommandStationEditor
    {
        public PreferredMotorolaCommandStationEditor() : base(AddressType.Motorola) { }
    }

    internal sealed class PreferredMfxCommandStationEditor : PreferredCommandStationEditor
    {
        public PreferredMfxCommandStationEditor() : base(AddressType.Mfx) { }
    }
}

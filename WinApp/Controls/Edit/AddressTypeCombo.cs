using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Combobox for AddressType enum
    /// </summary>
    public class AddressTypeCombo : EnumComboBox<AddressType>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public AddressTypeCombo()
            : base(ComboBoxStyle.DropDownList)
        {
            AddItem("DCC", AddressType.Dcc);
            AddItem("Loconet", AddressType.LocoNet);
            AddItem("MFX", AddressType.Mfx);
            AddItem("Motorola", AddressType.Motorola);
        }
    }
}

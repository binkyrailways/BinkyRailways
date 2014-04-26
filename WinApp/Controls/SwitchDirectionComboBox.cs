using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls
{
    public class SwitchDirectionComboBox : EnumComboBox<SwitchDirection>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchDirectionComboBox()
            : base(ComboBoxStyle.DropDownList, Default<SwitchDirectionTypeConverter>.Instance)
        {            
        }
    }
}

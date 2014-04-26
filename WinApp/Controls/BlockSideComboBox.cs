using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls
{
    public class BlockSideComboBox : EnumComboBox<BlockSide>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSideComboBox()
            : base(ComboBoxStyle.DropDownList, Default<BlockSideTypeConverter>.Instance)
        {            
        }
    }
}

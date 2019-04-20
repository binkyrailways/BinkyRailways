using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for BlockSide enum.
    /// </summary>
    public class BlockSideTypeConverter : EnumTypeConverter<BlockSide>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSideTypeConverter()
        {            
            AddItem(Strings.BlockSideFront, BlockSide.Front);
            AddItem(Strings.BlockSideBack, BlockSide.Back);
        }
    }
}

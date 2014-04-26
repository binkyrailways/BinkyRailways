using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for BlockSignalType enum.
    /// </summary>
    public class BlockSignalTypeTypeConverter : EnumTypeConverter<BlockSignalType>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSignalTypeTypeConverter()
        {            
            AddItem(Strings.BlockSignalTypeEntry, BlockSignalType.Entry);
            AddItem(Strings.BlockSignalTypeExit, BlockSignalType.Exit);
        }
    }
}

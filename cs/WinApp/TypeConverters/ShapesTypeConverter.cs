using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for BlockSide enum.
    /// </summary>
    public class ShapesTypeConverter: EnumTypeConverter<Shapes>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ShapesTypeConverter()
        {            
            AddItem(Strings.ShapesCircle, Shapes.Circle);
            AddItem(Strings.ShapesDiamond, Shapes.Diamond);
            AddItem(Strings.ShapesSquare, Shapes.Square);
            AddItem(Strings.ShapesTriangle, Shapes.Triangle);
        }
    }
}

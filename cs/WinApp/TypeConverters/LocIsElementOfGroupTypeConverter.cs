namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Type converter for "loc is element of group".
    /// </summary>
    public class LocIsElementOfGroupTypeConverter : BoolTypeConverter 
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocIsElementOfGroupTypeConverter()
            : base(Strings.LocIsElementOfGroup, Strings.LocIsNotElementOfGroup)
        {            
        }
    }
}

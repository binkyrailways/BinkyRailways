using System.ComponentModel;
using System.IO.Ports;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Type converter for COM port names
    /// </summary>
    public class ComPortNameTypeConverter : TypeConverter
    {
        /// <summary>
        /// Yes we support standard values
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Standard values are not exclusive
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        /// Gets standard values
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var portNames = SerialPort.GetPortNames();
            return new StandardValuesCollection(portNames);
        }
    }
}

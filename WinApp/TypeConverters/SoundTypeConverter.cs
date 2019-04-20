using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert a stream type sound to something else
    /// </summary>
    public class SoundTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string)) ||
                base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var stream = value as Stream;
            if (destinationType == typeof(string))
            {
                return stream == null ? Strings.None : "...";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

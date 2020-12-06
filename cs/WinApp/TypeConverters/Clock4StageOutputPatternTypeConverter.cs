using System;
using System.ComponentModel;
using System.Globalization;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Type converter for 4-stage clock output pattern.
    /// </summary>
    public class Clock4StageOutputPatternTypeConverter : TypeConverter
    {
        /// <summary>
        /// We can convert from string
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) { return true; }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// We can convert to string
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) { return true; }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Convert from string.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sValue = value as string;
            if (sValue != null)
            {
                sValue = sValue.Trim().Replace("-", "");
                var pattern = 0;
                for (int i = 0; i < 2; i++)
                {
                    var bit = (sValue.Length > i) ? sValue[i] : '0';
                    if (bit == '1')
                        pattern |= (1 << i);
                }
                return pattern;
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var pattern = (int)value;
                var result = string.Empty;
                for (int i = 0; i < 2; i++)
                {
                    if (i > 0)
                        result += "-";
                    result += ((pattern & (1 << i)) != 0) ? "1" : "0";
                }
                return result;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

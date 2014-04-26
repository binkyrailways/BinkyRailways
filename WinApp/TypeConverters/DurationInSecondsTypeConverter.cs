using System;
using System.ComponentModel;
using System.Globalization;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Type converter for duration (int, seconds) values.
    /// </summary>
    public class DurationInSecondsTypeConverter : TypeConverter
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
                sValue = sValue.Trim();
                var multiplier = 1;
                if (sValue.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                {
                    multiplier = 1;
                    sValue = sValue.Substring(0, sValue.Length - 1).Trim();
                }
                else if (sValue.EndsWith("m", StringComparison.OrdinalIgnoreCase))
                {
                    multiplier = 60;
                    sValue = sValue.Substring(0, sValue.Length - 1).Trim();
                }
                int x;
                if (int.TryParse(sValue, out x))
                {
                    return x * multiplier;
                }
                return 0;
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
                var iValue = (int)value;
                if ((iValue >= 60) && ((iValue % 60) == 0))
                {
                    var minutes = iValue/60;
                    return string.Format("{0}m", minutes);
                }
                return string.Format("{0}s", iValue);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Type converter for clock speed factor.
    /// </summary>
    public class ClockSpeedFactorTypeConverter : TypeConverter
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
                var index = sValue.IndexOf('(');
                if (index >= 0) sValue = sValue.Substring(0, index).Trim();
                int iValue;
                if (int.TryParse(sValue, out iValue))
                    return iValue;
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
                if (iValue == 0)
                    return "1";

                var totalSeconds = (24 * 60 * 60) / iValue;
                var seconds = totalSeconds % 60;
                var minutes = (totalSeconds / 60) % 60;
                var hours = (totalSeconds / (24 * 60)) % 24;

                var sb = new StringBuilder();
                sb.AppendFormat("{0} (24h -> ", iValue);
                if (hours > 0) sb.AppendFormat("{0}h ", hours);
                if ((hours > 0) || (minutes > 0)) sb.AppendFormat("{0}m ", minutes);
                if (seconds > 0) sb.AppendFormat("{0}s", seconds);

                return sb.ToString().Trim() + ")";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

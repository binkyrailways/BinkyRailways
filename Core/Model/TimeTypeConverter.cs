using System;
using System.ComponentModel;
using System.Globalization;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Type converter for model time 
    /// </summary>
    public sealed class TimeTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sValue = value as string;
            if (sValue != null)
            {
                Time tValue;
                if (Time.TryParse(sValue, out tValue))
                    return tValue;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var tValue = (Time)value;
                return tValue.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

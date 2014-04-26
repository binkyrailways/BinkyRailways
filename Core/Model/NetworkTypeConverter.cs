using System;
using System.ComponentModel;
using System.Globalization;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Identification of network on addresses of the same type.
    /// </summary>
    public sealed class NetworkTypeConverter : TypeConverter
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
                var parts = sValue.Split(new[] { ',' }, 2);
                var type = (AddressType)Enum.Parse(typeof (AddressType), parts[0]);
                var addressSpace = (parts.Length > 1) ? parts[1] : null;
                return new Network(type, addressSpace);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var aValue = value as Network;
                if (aValue == null)
                    return string.Empty;
                return aValue.Type + "," + aValue.AddressSpace;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

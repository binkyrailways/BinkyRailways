using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Identification of network on addresses of the same type.
    /// </summary>
    public sealed class AddressTypeConverter : TypeConverter
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
                if (sValue.Length == 0)
                    return null;

                var haveType = false;
                var haveValue = false;
                var haveAddressSpace = false;
                var type = AddressType.Dcc;
                var aValue = 1;
                string addressSpace = null;
                var typeNames = Enum.GetNames(typeof (AddressType)).Select(x => x.ToUpper()).ToList();

                while (sValue.Length > 0)
                {
                    var index = sValue.IndexOfAny(new[] {',', ' '});
                    var part = (index >= 0) ? sValue.Substring(0, index).Trim() : sValue.Trim();
                    sValue = (index >= 0) ? sValue.Substring(index + 1) : string.Empty;

                    if (!haveType)
                    {
                        if (typeNames.Contains(part.ToUpper()))
                        {
                            // It's and address type                            
                            type = (AddressType) Enum.Parse(typeof (AddressType), part, true);
                            haveType = true;
                            continue;                            
                        }
                    }
                    if (!haveValue)
                    {
                        int number;
                        if (int.TryParse(part, out number))
                        {
                            aValue = number;
                            haveValue = true;
                            continue;
                        }
                    }
                    if (!haveAddressSpace)
                    {
                        addressSpace = part;
                        haveAddressSpace = true;
                        continue;
                    }
                }

                return new Address(type, addressSpace, aValue);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var aValue = value as Address;
                if (aValue == null)
                    return string.Empty;
                var result = aValue.Type + " " + aValue.Value;
                if (!string.IsNullOrEmpty(aValue.AddressSpace))
                {
                    result += " " + aValue.AddressSpace;
                }
                return result;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

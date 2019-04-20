using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert junction with state set to string
    /// </summary>
    public class JunctionWithStateSetTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string)) ||
                base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var junctions = value as IJunctionWithStateSet;
                return ((junctions == null) || (junctions.Count == 0)) ? Strings.None :
                    string.Join(", ", junctions.Select(x => x.Junction.ToString()).ToArray());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

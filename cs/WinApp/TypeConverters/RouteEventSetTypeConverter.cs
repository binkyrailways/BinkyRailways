using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert sensor set to string
    /// </summary>
    public class RouteEventSetTypeConverter : TypeConverter
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
                var events = value as IRouteEventSet;
                return ((events == null) || (events.Count == 0)) ? Strings.None :
                    string.Join(", ", events.Select(x => x.ToString()).ToArray());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

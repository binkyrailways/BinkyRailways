using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert loc set to string
    /// </summary>
    public class LocSetTypeConverter : TypeConverter
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
                var locs = value as IEntitySet3<ILoc>;
                return ((locs == null) || (locs.Count == 0)) ? Strings.None :
                    string.Join(", ", locs.Select(x => x.ToString()).ToArray());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

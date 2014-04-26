using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert <see cref="UsedByInfos"/> to string
    /// </summary>
    public class UsedByTypeConverter : TypeConverter
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
                var infos = value as UsedByInfos;
                return ((infos == null) || (infos.Count == 0)) ? Strings.None : string.Join(", ", infos.Select(x => x.UsedBy.ToString()).ToArray());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

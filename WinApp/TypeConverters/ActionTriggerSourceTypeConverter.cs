using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert action trigger source to string
    /// </summary>
    public class ActionTriggerSourceTypeConverter : TypeConverter
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
                var source = value as IActionTriggerSource;
                var count = (source != null) ? source.Triggers.Sum(x => x.Count) : 0;
                return (count == 0) ? Strings.None : count.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

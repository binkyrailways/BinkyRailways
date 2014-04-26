using System;
using System.ComponentModel;
using System.Globalization;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert module entity to its global description
    /// </summary>
    public class GlobalEntityTypeConverter : TypeConverter
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
                var entity = value as IModuleEntity;
                return (entity == null) ? Strings.None : entity.GlobalDescription();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

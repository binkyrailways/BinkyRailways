using System;
using System.ComponentModel;
using System.Globalization;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert entity to string
    /// </summary>
    public class EntityTypeConverter : TypeConverter
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
                var entity = value as IEntity;
                return (entity == null) ? Strings.None : entity.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Generic type converter to enums.
    /// </summary>
    public class EnumTypeConverter<T> : TypeConverter
        where T : struct
    {
        private readonly List<T> values = new List<T>();
        private readonly Dictionary<T, string> enum2String = new Dictionary<T, string>();
        private readonly Dictionary<string, T> string2Enum = new Dictionary<string, T>();

        /// <summary>
        /// Default ctor
        /// </summary>
        protected EnumTypeConverter()
        {
            StandardValuesExclusive = true;
        }

        /// <summary>
        /// Add a text + value item.
        /// </summary>
        protected void AddItem(string text, T value)
        {
            string2Enum.Add(text, value);
            enum2String.Add(value, text);
            values.Add(value);
        }

        /// <summary>
        /// Add a text + value item.
        /// </summary>
        protected void AddItem(string text, T value, bool standard)
        {
            string2Enum.Add(text, value);
            if (standard)
            {
                enum2String.Add(value, text);
                values.Add(value);
            }
        }

        /// <summary>
        /// Is the standard values collection exclusive.
        /// True by default.
        /// </summary>
        protected bool StandardValuesExclusive { get; set; }

        /// <summary>
        /// Convenience convert to string property.
        /// </summary>
        public string this[T index]
        {
            get { return ConvertToString(index); }
        }

        /// <summary>
        /// Convenience convert from string property.
        /// </summary>
        public T this[string index]
        {
            get { return (T)ConvertFromString(index); }
        }

        /// <summary>
        /// We can convert from string
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) { return true; }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// We can convert to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) { return true; }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Convert from string.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sValue = value as string;
            if (sValue != null)
            {
                T enumValue;
                if (string2Enum.TryGetValue(sValue, out enumValue))
                {
                    return enumValue;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var enumValue = (T)value;
                string sValue;
                if (enum2String.TryGetValue(enumValue, out sValue))
                {
                    return sValue;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// We support standard values.
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Gets the standard values
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(values);
        }

        /// <summary>
        /// Is the standard values collection exclusive.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return StandardValuesExclusive;
        }
    }
}

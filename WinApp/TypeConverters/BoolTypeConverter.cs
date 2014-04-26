using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Generic type converter for booleans.
    /// </summary>
    public class BoolTypeConverter : TypeConverter
    {
        private readonly string[] trueTexts;
        private readonly string[] falseTexts;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BoolTypeConverter()
            : this(Strings.Yes, Strings.No)
        {            
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected BoolTypeConverter(string trueText, string falseText)
        {
            trueTexts = new[] { trueText, "1", "true", "yes" };
            falseTexts = new[] { falseText, "0", "false", "no" };
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
                if (trueTexts.Any(text => string.Equals(sValue, text, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return true;
                }
                if (falseTexts.Any(text => string.Equals(sValue, text, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return false;
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
                var bValue = (bool)value;
                return bValue ? trueTexts[0] : falseTexts[0];
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
            return new StandardValuesCollection(new[] { true, false });
        }

        /// <summary>
        /// Is the standard values collection exclusive.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

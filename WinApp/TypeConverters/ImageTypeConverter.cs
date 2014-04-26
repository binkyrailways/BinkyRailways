using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// Convert a stream type image to something else
    /// </summary>
    public class ImageTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(Image)) ||
                base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string)) ||
                (destinationType == typeof(Image)) ||
                base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;
            var image = value as Image;
            if (image != null)
            {
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return stream;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var stream = value as Stream;
            if (destinationType == typeof(string))
            {
                return stream == null ? Strings.None : "...";
            }
            if (destinationType == typeof(Image)) 
            {
                return stream != null ? Image.FromStream(stream) : null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

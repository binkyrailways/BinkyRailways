using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings helper methods
    /// </summary>
    internal static class SettingsExtensions
    {
        /// <summary>
        /// Add settings for the given property
        /// </summary>
        internal static PropertyDescriptor Add(this ExPropertyDescriptorCollection collection,
            Expression<Func<object>> member, string category, string displayName, string description)
        {
            var prop = Member.Of(member).AsProperty();
            var descriptor = TypeDescriptor.CreateProperty(prop.DeclaringType, prop.Name,
                prop.PropertyType, GetAttributes(collection, prop, null, category, displayName, description));
            collection.Add(descriptor);
            return descriptor;
        }

        /// <summary>
        /// Add settings for the given property
        /// </summary>
        internal static PropertyDescriptor Add<TComponent, TProperty>(this ExPropertyDescriptorCollection collection,
            Func<TComponent, TProperty> getter, Action<TComponent, TProperty> setter, TProperty defaultValue, string name, string category, string displayName, string description, params Attribute[] attributes)
        {
            var descriptor = new DynamicPropertyDescriptor<TComponent, TProperty>(name, getter, setter, defaultValue,
                GetAttributes(collection, null, attributes, category, displayName, description));
            collection.Add(descriptor);
            return descriptor;
        }

        /// <summary>
        /// Collect all attributes from the given property.
        /// Adjust / add category and display name
        /// </summary>
        private static Attribute[] GetAttributes(ExPropertyDescriptorCollection collection, PropertyInfo propertyInfo, IEnumerable<Attribute> attributes, string category, string displayName, string description)
        {
            var attrs = (propertyInfo != null) ? propertyInfo.GetCustomAttributes(false).Cast<Attribute>().ToList() : new List<Attribute>();
            if (attributes != null)
            {
                attrs.AddRange(attributes);
            }
            attrs.RemoveAll(x => x is CategoryAttribute);
            attrs.RemoveAll(x => x is DisplayNameAttribute);
            attrs.RemoveAll(x => x is DescriptionAttribute);
            attrs.Add(new CategoryAttribute(category));
            attrs.Add(new DisplayNameAttribute(displayName));
            attrs.Add(new DescriptionAttribute(description));

            if (collection.InRunningState)
            {
                // Mark property readonly when not attributed as editable in running state
                if (!attrs.OfType<EditableInRunningStateAttribute>().Any())
                {
                    attrs.RemoveAll(x => x is EditorAttribute);
                    attrs.Add(new ReadOnlyAttribute(true));
                    attrs.Add(new EditorAttribute(typeof(NoEditor), typeof(UITypeEditor)));
                }
            }

            return attrs.ToArray();
        }

        /// <summary>
        /// Editor used for readonly fields.
        /// </summary>
        private class NoEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.None;
            }
        }
    }
}

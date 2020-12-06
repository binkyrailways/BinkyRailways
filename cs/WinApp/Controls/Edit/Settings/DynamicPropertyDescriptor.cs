using System;
using System.ComponentModel;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    public sealed class DynamicPropertyDescriptor<TComponent, TProperty> : PropertyDescriptor
    {
        private readonly Func<TComponent, TProperty> getter;
        private readonly Action<TComponent, TProperty> setter;
        private readonly TProperty defaultValue;

        public DynamicPropertyDescriptor(string name, Func<TComponent, TProperty> getter, Action<TComponent, TProperty> setter, TProperty defaultValue, params Attribute[] attributes)
            : base(name, attributes)
        {
            this.getter = getter;
            this.setter = setter;
            this.defaultValue = defaultValue;
        }

        public override bool CanResetValue(object component)
        {
            return (setter != null);
        }

        public override object GetValue(object component)
        {
            return getter((TComponent) component);
        }

        public override void ResetValue(object component)
        {
            if (setter != null)
            {
                setter((TComponent) component, defaultValue);
            }
        }

        public override void SetValue(object component, object value)
        {
            if (setter != null)
            {
                setter((TComponent) component, (TProperty) value);
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return !Equals(defaultValue, getter((TComponent) component));
        }

        public override Type ComponentType
        {
            get { return typeof(TComponent); }
        }

        public override bool IsReadOnly
        {
            get { return (setter == null); }
        }

        public override Type PropertyType
        {
            get { return typeof(TProperty); }
        }
    }
}

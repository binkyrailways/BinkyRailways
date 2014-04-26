using System;
using System.ComponentModel;

namespace BinkyRailways.Core.ComponentModel
{
    public class SetEntryPropertyDescriptor : PropertyDescriptor
    {
        private readonly object stateSet;
        private readonly object stateObject;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SetEntryPropertyDescriptor(object stateSet, object stateObject, int index, int maxValue)
            :base(FormatName(index, maxValue), null)
        {
            this.stateSet = stateSet;
            this.stateObject = stateObject;
        }

        /// <summary>
        /// Gets the collection of attributes for this member.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.ComponentModel.AttributeCollection"/> that provides the attributes for this member, or an empty collection if there are no attributes in the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray"/>.
        /// </returns>
        public override AttributeCollection Attributes
        {
            get { return new AttributeCollection(null); }
        }

        /// <summary>
        /// When overridden in a derived class, returns whether resetting an object changes its value.
        /// </summary>
        /// <returns>
        /// true if resetting the component changes its value; otherwise, false.
        /// </returns>
        /// <param name="component">The component to test for reset capability. 
        ///                 </param>
        public override bool CanResetValue(object component)
        {
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, gets the current value of the property on a component.
        /// </summary>
        /// <returns>
        /// The value of a property for a given component.
        /// </returns>
        /// <param name="component">The component with the property for which to retrieve the value. 
        ///                 </param>
        public override object GetValue(object component)
        {
            return stateObject;
        }

        /// <summary>
        /// When overridden in a derived class, resets the value for this property of the component to the default value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be reset to the default value. 
        ///                 </param>
        public override void ResetValue(object component)
        {
            // Do nothing
        }

        /// <summary>
        /// When overridden in a derived class, sets the value of the component to a different value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be set. 
        ///                 </param><param name="value">The new value. 
        ///                 </param>
        public override void SetValue(object component, object value)
        {
            // Do nothing
        }

        /// <summary>
        /// When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
        /// </summary>
        /// <returns>
        /// true if the property should be persisted; otherwise, false.
        /// </returns>
        /// <param name="component">The component with the property to be examined for persistence. 
        ///                 </param>
        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the component this property is bound to.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"/> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"/> methods are invoked, the object specified might be an instance of this type.
        /// </returns>
        public override Type ComponentType
        {
            get { return stateSet.GetType(); }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this property is read-only.
        /// </summary>
        /// <returns>
        /// true if the property is read-only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the property.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the property.
        /// </returns>
        public override Type PropertyType
        {
            get { return stateObject.GetType(); }
        }

        private static string FormatName(int index, int maxValue)
        {
            var id = index.ToString();
            var digits = maxValue.ToString().Length;
            while (id.Length < digits)
            {
                id = "0" + id;
            }
            return "#" + id;
        }
    }
}

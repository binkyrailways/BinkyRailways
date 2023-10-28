using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace BinkyRailways.Core.ComponentModel
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public class CollectionTypeDescriptor : CustomTypeDescriptor
    {
        private readonly IEnumerable collection;

        /// <summary>
        /// Default ctor
        /// </summary>
        public CollectionTypeDescriptor(ICustomTypeDescriptor parent, IEnumerable collection) : base(parent)
        {
            this.collection = collection;
        }

        /// <summary>
        /// Returns the properties for this instance of a component.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that represents the properties for this component instance.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties()
        {
            var result = new PropertyDescriptorCollection(null);
            int index = 0;
            var count = collection.Cast<object>().Count();
            foreach (var state in collection)
            {
                result.Add(new SetEntryPropertyDescriptor(this, state, index++, count - 1));
            }
            return result;
        }

        public override PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            return GetProperties();
        }
    }
}

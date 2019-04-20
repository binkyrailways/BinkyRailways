using System;
using System.Collections;
using System.ComponentModel;

namespace BinkyRailways.Core.ComponentModel
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public class CollectionTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly TypeDescriptionProvider DefaultProvider = TypeDescriptor.GetProvider(typeof (IEnumerable));

        /// <summary>
        /// Default ctor
        /// </summary>
        public CollectionTypeDescriptionProvider()
            : base(DefaultProvider)
        {
        }

        /// <summary>
        /// Gets a custom type descriptor for the given type and object.
        /// </summary>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            var defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            if (instance == null)
            {
                return defaultDescriptor;
            }
            if (instance is IEnumerable)
            {
                return new CollectionTypeDescriptor(defaultDescriptor, (IEnumerable) instance);
            }
            return base.GetTypeDescriptor(objectType, instance);
        }
    }
}

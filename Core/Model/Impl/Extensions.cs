namespace BinkyRailways.Core.Model.Impl
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the ID of the given property.
        /// </summary>
        internal static string GetId<T>(this Property<EntityRef<T>> x) where T : class, IEntity
        {
            var value = x.Value;
            return (value != null) ? value.Id : null;
        }

        /// <summary>
        /// Validate the integrity of the value of the given property.
        /// No validation occurs if the property value is null.
        /// </summary>
        internal static void Validate<T>(this Property<T> property, ValidationResults results)
            where T : class, IEntity
        {
            var value = property.Value;
            if (value != null)
            {
                value.Validate(results);
            }
        }

        /// <summary>
        /// Validate the integrity of the value of the given property.
        /// No validation occurs if the property value is null.
        /// </summary>
        internal static void Validate<T>(this Property<EntityRef<T>> property, IEntity validationRoot, ValidationResults results)
            where T : class, IEntity
        {
            var value = property.Value;
            if (value != null)
            {
                value.Validate(validationRoot, results);
            }
        }
    }
}

using System;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Reference towards an entity.
    /// </summary>
    public class EntityRef<T>
        where T : class, IEntity
    {
        private readonly IEntity owner;
        private readonly Func<string, T> lookup;
        private readonly string id;
        private T item;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityRef(IEntity owner, T item)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            if (item == null)
                throw new ArgumentNullException("item");
            this.owner = owner;
            id = item.Id;
            this.item = item;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityRef(IEntity owner, string id, Func<string, T> lookup)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            this.owner = owner;
            this.id = id;
            this.lookup = lookup;
        }

        /// <summary>
        /// Gets the id of the item
        /// </summary>
        public string Id { get { return id; } }

        /// <summary>
        /// Gets the item, lookup if needed.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        public bool TryGetItem(out T result)
        {
            result = null;
            if (item == null)
            {
                item = lookup(id);
                if (item == null)
                    return false;
            }
            result = item;
            return true;
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public virtual void Validate(IEntity validationRoot, ValidationResults results)
        {
            if ((item == null) && (lookup(id) == null))
            {
                results.Error(owner, Strings.ErrFailedToLookupRefWithIdX, id);
            }
        }
    }
}

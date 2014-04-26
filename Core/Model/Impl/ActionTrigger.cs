using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that are invoked upon a changing sensor value.
    /// </summary>
    [XmlInclude(typeof(Action))]
    public sealed class ActionTrigger : EntityList<Action, IAction>, IActionTrigger
    {
        private readonly IActionTriggerSourceInternals owner;
        private readonly string description;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ActionTrigger(IActionTriggerSourceInternals owner, string description)
            : base(owner)
        {
            this.owner = owner;
            this.description = description;
        }

        /// <summary>
        /// Gets the human readable description of this trigger.
        /// </summary>
        public string Name { get { return description; } }

        /// <summary>
        /// Are there no elements?
        /// </summary>
        public bool IsEmpty { get { return (Count == 0); } }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(Action item)
        {
            base.OnAdded(item);
            item.Owner = owner;
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(Action item)
        {
            base.OnRemoved(item);
            item.Owner = null;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<IAction> IEnumerable<IAction>.GetEnumerator()
        {
            return Entries.Cast<IAction>().GetEnumerator();
        }

        /// <summary>
        /// Gets an element at the given index.
        /// </summary>
        IAction IEntityList<IAction>.this[int index]
        {
            get { return this[index]; }
        }

        /// <summary>
        /// Remove the given item from this list.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool IEntityList<IAction>.Remove(IAction item)
        {
            return Remove((Action)item);
        }

        /// <summary>
        /// Move the given item to the given new index.
        /// </summary>
        void IEntityList<IAction>.MoveTo(IAction item, int index)
        {
            MoveTo((Action)item, index);
        }

        /// <summary>
        /// Add the given action to this list.
        /// </summary>
        void IActionTrigger.Add(IAction action)
        {
            var item = (Action) action;
            if (item.Owner != null) 
                throw new ArgumentException("Action already has an owner");
            Add(item);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public void Validate(IEntity validationRoot, ValidationResults results)
        {
            foreach (var entity in this)
            {
                entity.Validate(validationRoot, results);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            foreach (IEntityInternals entity in this)
            {
                entity.CollectUsageInfo(subject, results);
            }
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        public void RemovedFromPackage(IPersistentEntity entity)
        {
            foreach (var iterator in this.Cast<IPackageListener>())
            {
                iterator.RemovedFromPackage(entity);
            }
        }
    }
}

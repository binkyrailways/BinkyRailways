using System;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that are invoked upon a changing sensor value.
    /// </summary>
    [XmlInclude(typeof(InitializeJunctionAction))]
    [XmlInclude(typeof(LocFunctionAction))]
    [XmlInclude(typeof(PlaySoundAction))]
    public abstract class Action : Entity, IAction
    {
        private IActionTriggerSourceInternals owner;

        /// <summary>
        /// Actions always generate their own description.
        /// </summary>
        public override bool HasAutomaticDescription
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the persistent entity that contains this action trigger.
        /// </summary>
        protected IPersistentEntity Container
        {
            get
            {
                var owner = Owner;
                if (owner == null)
                    throw new InvalidOperationException("No owner set");
                return owner.Container;
            }
        }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected override Railway Root
        {
            get
            {
                var owner = Owner;
                if (owner == null)
                    throw new InvalidOperationException("No owner set");
                return (Railway) owner.Railway;
            }
        }

        /// <summary>
        /// Gets the package that contains this action.
        /// </summary>
        protected IPackage Package
        {
            get
            {
                var root = (PersistentEntity)Container;
                return (root == null) ? null : root.Package;
            }
        }

        /// <summary>
        /// Owner of this action.
        /// This property is set by <see cref="ActionTrigger"/>.
        /// </summary>
        internal IActionTriggerSourceInternals Owner
        {
            get { return owner; }
            set
            {
                if (owner != value)
                {
                    owner = value;
                    OwnerChanged();
                }
            }
        }

        /// <summary>
        /// Called when the Owner property has changed.
        /// </summary>
        protected virtual void OwnerChanged()
        {
            // Nothing here
        }

        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        protected abstract Action Clone();

        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        IAction IAction.Clone()
        {
            return Clone();
        }
    }
}

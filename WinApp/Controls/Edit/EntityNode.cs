using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    internal class EntityNode : TreeNode
    {
        private readonly IEntity entity;
        private readonly bool archived;
        private readonly bool customText;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityNode(IEntity entity, ContextMenuStrip contextMenu)
            : this(entity, false, false)
        {
            if (contextMenu != null)
            {
                ContextMenuStrip = contextMenu;
            }
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityNode(IEntity entity, bool archived, bool customText)
        {
            this.entity = entity;
            this.archived = archived;
            this.customText = customText;
            UpdateFromEntity();
        }

        /// <summary>
        /// Is the given entity archive?
        /// </summary>
        public bool IsArchived
        {
            get { return archived; }
        }

        /// <summary>
        /// Entity displayed in this node
        /// </summary>
        public IEntity Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Gets the EntityRef of an <see cref="IEntityRefNode"/> node, otherwise
        /// returns <see cref="Entity"/>.
        /// </summary>
        public IEntity GetEntityRefOrEntity()
        {
            var node = this as IEntityRefNode;
            return (node != null) ? node.EntityRef : Entity;
        }

        /// <summary>
        /// Reload my state from the entity
        /// </summary>
        internal void UpdateFromEntity()
        {
            if (!customText)
            {
                Text = string.IsNullOrEmpty(entity.Description) ? entity.Id : entity.Description;
            }
        }
    }
}

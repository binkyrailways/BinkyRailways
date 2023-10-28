using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    internal interface IEntityRefNode
    {
        /// <summary>
        /// Entity reference
        /// </summary>
        IEntity EntityRef { get; }

        /// <summary>
        /// Actual Entity 
        /// </summary>
        IEntity Entity { get; }

        /// <summary>
        /// Is the given entity archive?
        /// </summary>
        bool IsArchived { get; }        
    }

    internal sealed class EntityRefNode<T> : EntityNode, IEntityRefNode
        where T : IEntity
    {
        private readonly IPersistentEntityRef<T> entityRef;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityRefNode(IPersistentEntityRef<T> entityRef, T entity)
            : base(entity, false, false)
        {
            this.entityRef = entityRef;
        }

        /// <summary>
        /// Entity reference
        /// </summary>
        public IPersistentEntityRef<T> EntityRef
        {
            get { return entityRef; }
        }

        /// <summary>
        /// Entity reference
        /// </summary>
        IEntity IEntityRefNode.EntityRef
        {
            get { return entityRef; }
        }
    }
}

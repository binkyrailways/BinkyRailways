using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Maintains selected entities
    /// </summary>
    internal sealed class SelectionManager
    {
        /// <summary>
        /// Fired when the selection has changed.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Actual selection
        /// </summary>
        private readonly HashSet<IEntity> selection = new HashSet<IEntity>();

        /// <summary>
        /// If larger than 1 changed notifications are blocked
        /// </summary>
        private int updateCounter;

        /// <summary>
        /// A blocked change notification is pending in an update.
        /// </summary>
        private bool changedPending;

        /// <summary>
        /// De-select all
        /// </summary>
        public void Clear()
        {
            if (selection.Count == 0)
                return;
            selection.Clear();
            OnChanged();
        }

        /// <summary>
        /// Add the given entity to the selection
        /// </summary>
        public void Add(IEntity entity)
        {
            if (selection.Add(entity))
            {
                OnChanged();
            }
        }

        /// <summary>
        /// Remove the given entity to the selection
        /// </summary>
        public void Remove(IEntity entity)
        {
            if (selection.Remove(entity))
            {
                OnChanged();
            }
        }

        /// <summary>
        /// Is the given entity selected?
        /// </summary>
        public bool Contains(IEntity entity)
        {
            return selection.Contains(entity);
        }

        /// <summary>
        /// Gets all selected entities
        /// </summary>
        public IEnumerable<IEntity> SelectedEntities
        {
            get { return selection; }
        }

        /// <summary>
        /// Gets the number of selected entities
        /// </summary>
        public int Count
        {
            get { return selection.Count; }
        }

        /// <summary>
        /// Block change notifications until the returned object is disposed.
        /// </summary>
        public IDisposable BeginUpdate()
        {
            updateCounter++;
            return new Update(this, updateCounter);
        }

        /// <summary>
        /// Resume change notifications
        /// </summary>
        private void EndUpdate(int expectedValue)
        {
            if (updateCounter != expectedValue)
                throw new InvalidOperationException("Mismatch in update counter");
            updateCounter--;
            if ((updateCounter == 0) && (changedPending))
            {
                changedPending = false;
                OnChanged();
            }
        }

        /// <summary>
        /// Update dependencies and fire Changed event.
        /// </summary>
        private void OnChanged()
        {
            if (updateCounter > 0)
            {
                changedPending = true;
                return;
            }
            Changed.Fire(this);
        }

        private sealed class Update : IDisposable
        {
            private readonly SelectionManager selectionManager;
            private readonly int updateCounter;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Update(SelectionManager selectionManager, int updateCounter)
            {
                this.selectionManager = selectionManager;
                this.updateCounter = updateCounter;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            void IDisposable.Dispose()
            {
                selectionManager.EndUpdate(updateCounter);
            }
        }
    }
}

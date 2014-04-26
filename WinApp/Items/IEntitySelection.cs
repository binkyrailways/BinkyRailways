using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Selection interface between items and the app.
    /// </summary>
    public interface IEntitySelection
    {
        /// <summary>
        /// The selection has changed.
        /// </summary>
        event EventHandler SelectionChanged;

        /// <summary>
        /// Make the given entity the selection.
        /// </summary>
        void SetSelection(IEntity entity);

        /// <summary>
        /// Are there selected entities?
        /// </summary>
        bool HasSelection { get; }

        /// <summary>
        /// Gets all selected entities.
        /// </summary>
        IEnumerable<IEntity> SelectedEntities { get; }
    }
}

using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Items
{
    public class ItemContext
    {
        /// <summary>
        /// Fired when a setting has changed.
        /// </summary>
        public event EventHandler Changed;

        private readonly ItemContext parent;
        private readonly Func<IRailway> getRailway;
        private readonly Func<IEnumerable<IEntity>> getSelectedEntities;
        private readonly Action reload;
        private readonly Action<ILocState> selectLoc;
        private bool showDescriptions = true;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ItemContext(ItemContext parent, Func<IRailway> getRailway, Func<IEnumerable<IEntity>> getSelectedEntities, Action reload, Action<ILocState> selectLoc)
        {
            this.parent = parent;
            this.getRailway = getRailway;
            this.getSelectedEntities = getSelectedEntities;
            this.reload = reload;
            this.selectLoc = selectLoc;
        }

        /// <summary>
        /// Show descriptions of non-block items?
        /// </summary>
        public bool ShowDescriptions
        {
            get { return (parent != null) ? parent.ShowDescriptions : showDescriptions; }
            set
            {
                if (parent != null)
                {
                    if (parent.ShowDescriptions != value)
                    {
                        parent.ShowDescriptions = value;
                        Changed.Fire(this);
                    }
                }
                else
                {
                    if (showDescriptions != value)
                    {
                        showDescriptions = value;
                        Changed.Fire(this);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current railway
        /// </summary>
        public IRailway Railway
        {
            get { return (parent != null) ? parent.Railway : getRailway(); }
        }
        
        /// <summary>
        /// Gets all selected entities.
        /// </summary>
        public IEnumerable<IEntity> SelectedEntities
        {
            get { return getSelectedEntities(); }
        }

        /// <summary>
        /// Used in edge connection.
        /// </summary>
        public IEdge SelectedEdge { get; set; }

        /// <summary>
        /// Force a reload of the view.
        /// </summary>
        public void Reload()
        {
            reload();
        }

        /// <summary>
        /// Select the given loc.
        /// </summary>
        public void SelectLoc(ILocState loc)
        {
            if (selectLoc != null)
            {
                selectLoc(loc);
            }
        }
    }
}

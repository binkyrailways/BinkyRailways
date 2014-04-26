using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Layout;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Items.Edit;

namespace BinkyRailways.WinApp.Controls.Edit
{
    public partial class RailwayViewEditorControl : UserControl
    {
        public event EventHandler SelectionChanged;
        public event EventHandler Reload;

        private AppState appState;
        private readonly ItemContext context;
        private RailwayEditItem railwayItem;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayViewEditorControl()
        {
            context = new ItemContext(null, () => Railway, () => SelectedEntities, () => Reload.Fire(this));
            InitializeComponent();
            context.Changed += OnContextChanged;
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            this.appState = appState;
            appState.PackageChanged += (s, _) => ReloadView();
            ReloadView();
        }

        /// <summary>
        /// Item context has changed.
        /// </summary>
        private void OnContextChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        /// <summary>
        /// Gets the context given to all items.
        /// </summary>
        internal ItemContext Context { get { return context; } }

        /// <summary>
        /// Reload the canvas.
        /// </summary>
        internal void ReloadView()
        {
            using (canvas.BeginUpdate())
            {
                canvas.Items.Clear();
                if (railwayItem != null)
                {
                    railwayItem.SelectionChanged -= OnModuleSelectionChanged;
                    railwayItem = null;
                }
                var package = (appState != null) ? appState.Package : null; 
                var railway = (package != null) ? package.Railway : null;
                if (railway != null)
                {
                    railwayItem = new RailwayEditItem(railway, context);
                    canvas.Items.Add(railwayItem, new LayoutConstraints(FillDirection.Both));
                    railwayItem.SelectionChanged += OnModuleSelectionChanged;
                }
            }
        }

        /// <summary>
        /// Selected item has changed.
        /// </summary>
        private void OnModuleSelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged.Fire(this);
        }

        /// <summary>
        /// Make the given entity the selection.
        /// </summary>
        public void SetSelection(IEntity entity)
        {
            if (railwayItem != null)
            {
                railwayItem.SetSelection(entity);
            }
        }

        /// <summary>
        /// Are there selected entities?
        /// </summary>
        public bool HasSelection
        {
            get { return (railwayItem != null) && railwayItem.HasSelection; }
        }

        /// <summary>
        /// Gets all selected entities.
        /// </summary>
        public IEnumerable<IEntity> SelectedEntities
        {
            get { return (railwayItem != null) ? railwayItem.SelectedEntities : Enumerable.Empty<IEntity>(); }
        }

        /// <summary>
        /// Gets the railway
        /// </summary>
        private IRailway Railway
        {
            get { return (railwayItem != null) ? railwayItem.Railway : null; }
        }
    }
}

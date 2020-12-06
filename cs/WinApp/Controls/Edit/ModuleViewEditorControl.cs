using System;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Items.Edit;
using BinkyRailways.WinApp.Items.Run;

namespace BinkyRailways.WinApp.Controls.Edit
{
    public partial class ModuleViewEditorControl : UserControl
    {
        private AppState appState;
        private IModule module;
        private ModuleItem moduleItem;
        private ItemContext context;
        private bool runView;
        private SelectionManager selectionManager;
        private bool updatingSelection;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleViewEditorControl()
        {
            SelectionManager = new SelectionManager();
            InitializeComponent();
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState, IModule module, ItemContext context, bool runView)
        {
            this.context = context;
            this.appState = appState;
            this.module = module;
            this.runView = runView;
            ReloadModule();
            context.Changed += OnContextChanged;
        }

        /// <summary>
        /// Entity selection manager
        /// </summary>
        internal SelectionManager SelectionManager
        {
            get { return selectionManager; }
            set
            {
                if (value ==  null)
                    throw new ArgumentNullException("value");
                if (selectionManager != null)
                {
                    selectionManager.Changed -= OnSelectionManagerChanged;
                }
                selectionManager = value;
                selectionManager.Changed += OnSelectionManagerChanged;
            }
        }

        /// <summary>
        /// Item context has changed.
        /// </summary>
        private void OnContextChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        /// <summary>
        /// Reload the canvas.
        /// </summary>
        internal void ReloadModule()
        {
            using (canvas.BeginUpdate())
            {
                canvas.Items.Clear();
                if (moduleItem != null)
                {
                    moduleItem.SelectionChanged -= OnModuleItemSelectionChanged;
                    moduleItem = null;
                }

                if (module != null)
                {
                    if (runView)
                        moduleItem = new ModuleRunItem(appState.RailwayState, null, module, true, context);
                    else
                        moduleItem = new ModuleEditItem(null, module, true, false, context);
                    canvas.Items.Add(moduleItem, null);
                    moduleItem.SelectionChanged += OnModuleItemSelectionChanged;
                }
            }
        }

        /// <summary>
        /// Selected item has changed.
        /// </summary>
        private void OnModuleItemSelectionChanged(object sender, EventArgs e)
        {
            if (updatingSelection)
                return;
            var selMgr = SelectionManager;
            using (selMgr.BeginUpdate())
            {
                selMgr.Clear();
                if (moduleItem != null)
                {
                    foreach (var entity in moduleItem.SelectedEntities)
                    {
                        selMgr.Add(entity);
                    }
                }
            }
        }

        /// <summary>
        /// Select in the selection manager has changed.
        /// </summary>
        private void OnSelectionManagerChanged(object sender, EventArgs eventArgs)
        {
            if (updatingSelection)
                return;
            try
            {
                updatingSelection = true;
                if (moduleItem != null)
                {
                    moduleItem.SelectedEntities = selectionManager.SelectedEntities;
                }
            }
            finally
            {
                updatingSelection = false;
            }
        }
    }
}

using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    internal sealed class GridContext
    {
        private readonly AppState appState;
        private readonly IModule module;
        private readonly IEntity entityRef;
        private readonly Action reloadView;

        /// <summary>
        /// Default ctor
        /// </summary>
        public GridContext(AppState appState, IModule module, Action reloadView)
            : this(appState, module, null, reloadView)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public GridContext(AppState appState, IModule module, IEntity entityRef, Action reloadView)
        {
            this.appState = appState;
            this.entityRef = entityRef;
            this.reloadView = reloadView;
            this.module = module;
        }

        /// <summary>
        /// Reference to the visited entity
        /// </summary>
        public IEntity EntityRef
        {
            get { return entityRef; }
        }

        /// <summary>
        /// Module being edited.
        /// </summary>
        public IModule Module
        {
            get { return module; }
        }

        /// <summary>
        /// State of application
        /// </summary>
        public AppState AppState
        {
            get { return appState; }
        }

        /// <summary>
        /// Force a reload of the view.
        /// </summary>
        public void ReloadView()
        {
            if (reloadView != null)
            {
                reloadView();
            }
        }
    }
}

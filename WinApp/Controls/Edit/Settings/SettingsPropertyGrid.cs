using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Custom property grid for settings.
    /// </summary>
    public class SettingsPropertyGrid : PropertyGrid
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        private ServiceSite site;

        /// <summary>
        /// Is the railway in running state?
        /// </summary>
        public bool InRunningState { get; set; }

        /// <summary>
        /// When overridden in a derived class, enables the creation of a <see cref="T:System.Windows.Forms.Design.PropertyTab"/>.
        /// </summary>
        protected override PropertyTab CreatePropertyTab(Type tabType)
        {
            return new SettingsPropertyTab(this);
        }

        /// <summary>
        /// Register a new service for type T.
        /// </summary>
        internal void RegisterService<T>(T service)
        {
            services[typeof (T)] = service;
        }

        /// <summary>
        /// Get a service.
        /// </summary>
        protected override object GetService(Type service)
        {
            object result;
            return services.TryGetValue(service, out result) ? result : base.GetService(service);
        }

        /// <summary>
        /// Custom site to intercept GetService properly.
        /// </summary>
        public override ISite Site
        {
            get
            {
                if (DesignMode)
                    return base.Site;
                return site ?? (site = new ServiceSite(this));
            }
            set
            {
                base.Site = value;
            }
        }

        /// <summary>
        /// Custom tab for the properties of settings.
        /// </summary>
        public class SettingsPropertyTab : PropertyTab
        {
            private readonly SettingsPropertyGrid grid;

            /// <summary>
            /// Default ctor
            /// </summary>
            public SettingsPropertyTab(SettingsPropertyGrid grid)
            {
                this.grid = grid;
            }

            /// <summary>
            /// Gets the properties of the specified component that match the specified attributes.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that contains the properties.
            /// </returns>
            /// <param name="component">The component to retrieve properties from. 
            ///                 </param><param name="attributes">An array of type <see cref="T:System.Attribute"/> that indicates the attributes of the properties to retrieve. 
            ///                 </param>
            public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
            {
                var result = new ExPropertyDescriptorCollection(grid.InRunningState);
                var settings = component as IGatherProperties;
                if (settings != null)
                {
                    settings.GatherProperties(result);
                }
                return result;
            }

            /// <summary>
            /// Gets the name for the property tab.
            /// </summary>
            /// <returns>
            /// The name for the property tab.
            /// </returns>
            public override string TabName
            {
                get { return "My Properties"; }
            }

            /// <summary>
            /// Gets the bitmap that is displayed for the <see cref="T:System.Windows.Forms.Design.PropertyTab"/>.
            /// </summary>
            public override System.Drawing.Bitmap Bitmap
            {
                get { return BinkyRailways.Properties.Resources.document_properties_16; }
            }
        }

        private class ServiceSite : ISite
        {
            private readonly SettingsPropertyGrid grid;
            private readonly Container container = new Container();

            public ServiceSite(SettingsPropertyGrid grid)
            {
                this.grid = grid;
                Name = "SettingsSite";
            }

            public object GetService(Type serviceType)
            {
                return grid.GetService(serviceType);
            }

            public IComponent Component { get { return grid; } }
            public IContainer Container { get { return container; } }
            public bool DesignMode { get { return false; } }
            public string Name { get; set; }
        }
    }
}

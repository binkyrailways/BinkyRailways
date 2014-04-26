using System;
using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Base settings class for persistent entities.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class PersistentEntitySettings<T> : EntitySettings<T>
        where T : class, IPersistentEntity
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal PersistentEntitySettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => LastModified, Strings.TabGeneral, Strings.LastModifiedName, Strings.LastModifiedHelp);
        }

        /// <summary>
        /// Gets last modification date in local time.
        /// </summary>
        [MergableProperty(false)]
        public DateTime LastModified
        {
            get { return Entity.LastModified; }
        }
    }
}

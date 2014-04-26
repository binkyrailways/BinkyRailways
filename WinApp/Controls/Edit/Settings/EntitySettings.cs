using System.ComponentModel;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Base class for settings types.
    /// Settings types are used in a property grid to edit settings of an entity.
    /// </summary>
    internal interface IGatherProperties
    {
        /// <summary>
        /// Gets the current railway
        /// </summary>
        IRailway Railway { get; }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        void GatherProperties(ExPropertyDescriptorCollection properties);
    }

    /// <summary>
    /// Base class for settings types.
    /// Settings types are used in a property grid to edit settings of an entity.
    /// </summary>
    internal interface IEntitySettings : IGatherProperties
    {
        /// <summary>
        /// Gets the module being edited
        /// </summary>
        IModule Module { get; }

        /// <summary>
        /// Gets the context in which this setting is created.
        /// </summary>
        GridContext Context { get; }
    }

    /// <summary>
    /// Base class for settings types.
    /// Settings types are used in a property grid to edit settings of an entity.
    /// </summary>
    internal abstract class EntitySettings<T> : IEntitySettings
        where T : class, IEntity
    {
        protected readonly T Entity;
        protected readonly GridContext Context;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected EntitySettings(T entity, GridContext context)
        {
            Entity = entity;
            Context = context;
        }

        /// <summary>
        /// Gets the current railway
        /// </summary>
        IRailway IGatherProperties.Railway { get { return Context.AppState.Package.Railway; } }

        /// <summary>
        /// Gets the module being edited
        /// </summary>
        IModule IEntitySettings.Module { get { return Context.Module; } }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public virtual void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            if (ShowDescription)
            {
                properties.Add(() => Description, Strings.TabGeneral, Strings.DescriptionName, Strings.DescriptionHelp);
            }
            if (ShowUsedBy)
            {
                properties.Add(() => UsedBy, Strings.TabUsage, Strings.UsedByName, Strings.UsedByHelp);
            }
        }

        /// <summary>
        /// Gets the context in which this setting is created.
        /// </summary>
        GridContext IEntitySettings.Context { get { return Context; } }

        /// <summary>
        /// Name of the entity
        /// </summary>
        [Category("General")]
        [MergableProperty(false)]
        public string Description
        {
            get { return Entity.Description; }
            set { Entity.Description = value; }
        }

        /// <summary>
        /// Should the description property be shown?
        /// </summary>
        protected virtual bool ShowDescription { get { return true; } }

        /// <summary>
        /// Where is this entity used
        /// </summary>
        [TypeConverter(typeof(UsedByTypeConverter))]
        [MergableProperty(false)]
        public UsedByInfos UsedBy
        {
            get { return Entity.GetUsageInfo(); }
        }

        /// <summary>
        /// Should the UsedBy property be shown?
        /// </summary>
        protected virtual bool ShowUsedBy { get { return true; } }
    }
}

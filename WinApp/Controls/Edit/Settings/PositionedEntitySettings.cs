using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class PositionedEntitySettings<T> : EntitySettings<T>
        where T : class, IPositionedEntity
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected PositionedEntitySettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => X, Strings.TabPosition, Strings.XName, Strings.XHelp);
            properties.Add(() => Y, Strings.TabPosition, Strings.YName, Strings.YHelp);
            if (ShowSize)
            {
                properties.Add(() => Width, Strings.TabPosition, Strings.WidthName, Strings.WidthHelp);
                properties.Add(() => Height, Strings.TabPosition, Strings.HeightName, Strings.HeightHelp);
            }
            properties.Add(() => Rotation, Strings.TabPosition, Strings.RotationName, Strings.RotationHelp);
            properties.Add(() => Locked, Strings.TabPosition, Strings.LockedName, Strings.LockedHelp);
        }

        /// <summary>
        /// Horizontal left position (in pixels) of this entity.
        /// </summary>
        [Category("Position")]
        public int X
        {
            get { return Entity.X; }
            set { Entity.X = value; }
        }

        /// <summary>
        /// Vertical top position (in pixels) of this entity.
        /// </summary>
        [Category("Position")]
        public int Y
        {
            get { return Entity.Y; }
            set { Entity.Y = value; }
        }

        /// <summary>
        /// Horizontal size (in pixels) of this entity.
        /// </summary>
        [Category("Position")]
        public int Width
        {
            get { return Entity.Width; }
            set { Entity.Width = value; }
        }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        [Category("Position")]
        public int Height
        {
            get { return Entity.Height; }
            set { Entity.Height = value; }
        }

        /// <summary>
        /// Rotation of the contents of this entity in degrees.
        /// </summary>
        [Category("Position")]
        public int Rotation
        {
            get { return Entity.Rotation; }
            set { Entity.Rotation = value; }
        }

        /// <summary>
        /// If set, the mouse will no longer move and/or resize this entity.
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        [DefaultValue(false)]
        public bool Locked
        {
            get { return Entity.Locked; }
            set { Entity.Locked = value; }
        }

        /// <summary>
        /// Should width and height be shown?
        /// </summary>
        protected virtual bool ShowSize { get { return true; }}
    }
}

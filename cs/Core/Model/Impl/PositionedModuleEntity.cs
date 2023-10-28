using System;
using System.ComponentModel;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for entities contained in a module with a position.
    /// </summary>
    public abstract class PositionedModuleEntity : ModuleEntity
    {
        /// <summary>
        /// The position or size of this entity has changed.
        /// </summary>
        public event EventHandler PositionChanged;

        private const int DefaultX = 0;
        private const int DefaultY = 0;
        private const int MinWidth = 5;
        private const int MinHeight = 5;

        private readonly Property<int> x;
        private readonly Property<int> y;
        private readonly Property<int> width;
        private readonly Property<int> height;
        private readonly Property<int> rotation;
        private readonly Property<bool> locked;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected PositionedModuleEntity(int defaultWidth, int defaultHeight)
        {
            x = new Property<int>(this, DefaultX, OnPositionChanged);
            y = new Property<int>(this, DefaultY, OnPositionChanged);
            width = new Property<int>(this, defaultWidth, OnPositionChanged);
            height = new Property<int>(this, defaultHeight, OnPositionChanged);
            rotation = new Property<int>(this, 0, OnPositionChanged);
            locked = new Property<bool>(this, false);
        }

        /// <summary>
        /// Horizontal left position (in pixels) of this entity.
        /// </summary>
        [DefaultValue(DefaultX)]
        public int X
        {
            get { return x.Value; }
            set { x.Value = value; }
        }

        /// <summary>
        /// Vertical top position (in pixels) of this entity.
        /// </summary>
        [DefaultValue(DefaultY)]
        public int Y
        {
            get { return y.Value; }
            set { y.Value = value; }
        }

        /// <summary>
        /// Horizontal size (in pixels) of this entity.
        /// </summary>
        public int Width
        {
            get { return width.Value; }
            set { width.Value = Math.Max(MinWidth, value); }
        }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        public int Height
        {
            get { return height.Value; }
            set { height.Value = Math.Max(MinHeight, value); }
        }

        /// <summary>
        /// Rotation in degrees of the content of this entity.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRotation)]
        public int Rotation
        {
            get { return rotation.Value; }
            set { rotation.Value = value % 360; }
        }

        /// <summary>
        /// If set, the mouse will no longer move and/or resize this entity.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocked)]
        public bool Locked
        {
            get { return locked.Value; }
            set { locked.Value = value; }
        }

        /// <summary>
        /// Fire the PositionChanged event
        /// </summary>
        private void OnPositionChanged(int value)
        {
            PositionChanged.Fire(this);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (X < 0)
                results.Warn(this, Strings.WarnNegativeXPosition);
            if (Y < 0)
                results.Warn(this, Strings.WarnNegativeYPosition);
            if (Width <= 0)
                results.Warn(this, Strings.WarnWidthToSmall);
            if (Height <= 0)
                results.Warn(this, Strings.WarnHeightToSmall);
        }
    }
}

using System;
using System.ComponentModel;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Single module reference.
    /// </summary>
    public sealed class ModuleRef : PersistentEntityRef<IModule>, IModuleRef
    {
        /// <summary>
        /// The position or size of this entity has changed.
        /// </summary>
        public event EventHandler PositionChanged;

        private const int DefaultX = 0;
        private const int DefaultY = 0;

        private readonly Property<int> x;
        private readonly Property<int> y;
        private readonly Property<int> rotation;
        private readonly Property<bool> locked;
        private readonly Property<int> zoomFactor; 

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleRef()
        {
            x = new Property<int>(this, DefaultX, OnPositionChanged);
            y = new Property<int>(this, DefaultY, OnPositionChanged);
            rotation = new Property<int>(this, DefaultValues.DefaultRotation, OnPositionChanged);
            locked = new Property<bool>(this, DefaultValues.DefaultLocked);
            zoomFactor = new Property<int>(this, DefaultValues.DefaultModuleRefZoomFactor, OnPositionChanged);
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
        int IPositionedEntity.Width
        {
            get
            {
                IModule module;
                return TryResolve(out module) ? module.Width : 0;
            }
            set { /* ignore */ }
        }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        int IPositionedEntity.Height
        {
            get
            {
                IModule module;
                return TryResolve(out module) ? module.Height : 0;
            }
            set { /* ignore */ }
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
        /// Zoomfactor used in displaying the module (in percentage).
        /// </summary>
        /// <value>100 means 100%</value>
        [DefaultValue(DefaultValues.DefaultModuleRefZoomFactor)]
        public int ZoomFactor
        {
            get { return zoomFactor.Value; }
            set { zoomFactor.Value = Math.Max(1, value); }
        }

        /// <summary>
        /// Is this module a reference to the given module?
        /// </summary>
        public bool IsReferenceTo(IModule module)
        {
            return (module != null) && (module.Id == Id);
        }

        /// <summary>
        /// Try to resolve the entity.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        public override bool TryResolve(out IModule entity)
        {
            entity = null;
            var railway = Railway;
            if (railway == null)
                return false;
            entity = railway.Package.GetModule(Id);
            return (entity != null);
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Fire the PositionChanged event
        /// </summary>
        private void OnPositionChanged(int value)
        {
            PositionChanged.Fire(this);
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Place in the railway which is occupied by a single train and where a train can stop.
    /// </summary>
    [XmlInclude(typeof(LocStandardPredicate))]
    public sealed class Block : EndPoint, IBlock
    {

        private readonly Property<int> waitProbability;
        private readonly Property<int> minimumWaitTime;
        private readonly Property<int> maximumWaitTime;
        private LocStandardPredicate waitPermissions;
        private readonly Property<bool> reverseSides;
        private readonly Property<ChangeDirection> changeDirection;
        private readonly Property<bool> changeDirectionReversingLocs;
        private readonly Property<StationMode> stationMode;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Block()
            : base(32, 16)
        {
            waitProbability = new Property<int>(this, DefaultValues.DefaultBlockWaitProbability);
            minimumWaitTime = new Property<int>(this, DefaultValues.DefaultBlockMinimumWaitTime);
            maximumWaitTime = new Property<int>(this, DefaultValues.DefaultBlockMaximumWaitTime);
            waitPermissions = new LocStandardPredicate();
            waitPermissions.EnsureId();
            reverseSides = new Property<bool>(this, false);
            changeDirection = new Property<ChangeDirection>(this, DefaultValues.DefaultBlockChangeDirection);
            changeDirectionReversingLocs = new Property<bool>(this, DefaultValues.DefaultBlockChangeDirectionReversingLocs);
            stationMode = new Property<StationMode>(this, DefaultValues.DefaultBlockStationMode);
        }

        /// <summary>
        /// Always wait a while when this block is reached?
        /// Typically set for stations.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockWaitProbability)]
        public int WaitProbability
        {
            get { return waitProbability.Value; }
            set { waitProbability.Value = Math.Max(0, Math.Min(value, 100)); }
        }

        /// <summary>
        /// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockMinimumWaitTime)]
        public int MinimumWaitTime
        {
            get { return minimumWaitTime.Value; }
            set { minimumWaitTime.Value = Math.Max(value, 0); }
        }

        /// <summary>
        /// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockMaximumWaitTime)]
        public int MaximumWaitTime
        {
            get { return maximumWaitTime.Value; }
            set { maximumWaitTime.Value = Math.Max(value, 0); }
        }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to wait in this block.
        /// </summary>
        public LocStandardPredicate WaitPermissions
        {
            get { return waitPermissions; }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                // Store is used only for serialization.
                if (value == null)
                    throw new ArgumentNullException("waitPermission");
                value.Module = Module;
                value.EnsureId();
                waitPermissions = value;
            }
        }

        /// <summary>
        /// Should <see cref="WaitPermissions"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeWaitPermissions()
        {
            return !waitPermissions.IsEmpty;
        }

        /// <summary>
        /// By default the front of the block is on the right of the block.
        /// When this property is set, that is reversed to the left of the block.
        /// Setting this property will only alter the display behavior of the block.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockReverseSides)]
        public bool ReverseSides
        {
            get { return reverseSides.Value; }
            set { reverseSides.Value = value; }
        }

        /// <summary>
        /// Is it allowed for locs to change direction in this block?
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockChangeDirection)]
        public ChangeDirection ChangeDirection
        {
            get { return changeDirection.Value; }
            set { changeDirection.Value = value; }
        }

        /// <summary>
        /// Must reversing locs change direction (back to normal) in this block?
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockChangeDirectionReversingLocs)]
        public bool ChangeDirectionReversingLocs
        {
            get { return changeDirectionReversingLocs.Value; }
            set { changeDirectionReversingLocs.Value = value; }
        }

        /// <summary>
        /// Determines how the system decides if this block is part of a station
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockStationMode)]
        public StationMode StationMode
        {
            get { return stationMode.Value; }
            set { stationMode.Value = value; }
        }

        /// <summary>
        /// Is this block considered a station?
        /// </summary>
        public bool IsStation
        {
            get
            {
                switch (StationMode)
                {
                    case StationMode.Always:
                        return true;
                    case StationMode.Never:
                        return false;
                    default:
                        if (ChangeDirection == ChangeDirection.Allow) return (WaitProbability >= 50);
                        return (WaitProbability >= 75);
                }
            }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (WaitProbability > 0)
            {
                if (MinimumWaitTime > MaximumWaitTime)
                {
                    results.Warn(this, Strings.WarnBlockMinWaitTimeGreatThenMaxWaitTime);
                }
            }
            waitPermissions.Validate(validationRoot, results);
            var hasRoutesToMe = Module.Routes.Any(x => x.To == this);
            var hasRoutesFromMe = Module.Routes.Any(x => x.From == this);

            if ((!hasRoutesFromMe) && (!hasRoutesToMe))
            {
                results.Warn(this, Strings.WarnBlockNoRoutesToOrFromMe);
            } else if (!hasRoutesFromMe)
            {
                results.Warn(this, Strings.WarnBlockNoRoutesFromMe);
            }
            else if (!hasRoutesToMe)
            {
                results.Warn(this, Strings.WarnBlockNoRoutesToMe);
            }

        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            ((IPackageListener)waitPermissions).RemovedFromPackage(entity);
        }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to wait in this block.
        /// </summary>
        ILocStandardPredicate IBlock.WaitPermissions { get { return WaitPermissions; } }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return base.Module; }
            set
            {
                base.Module = value;
                waitPermissions.Module = value;
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameBlock; }
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BinkyRailways.Core.Storage;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    [XmlRoot]
    [XmlInclude(typeof(LocPredicate))]
    public partial class Module : PersistentEntity, IModule
    {
        private const string BackgroundImageId = "BackgroundImage";

        private readonly BlockSet blocks;
        private readonly BlockGroupSet blockGroups;
        private readonly JunctionSet junctions;
        private readonly RouteSet routes;
        private readonly EdgeSet edges;
        private readonly SensorSet sensors;
        private readonly SignalSet signals;
        private readonly OutputSet outputs;
        private Size? backgroundImageSize;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Module()
        {
            blocks = new BlockSet(this);
            blockGroups = new BlockGroupSet(this);
            junctions = new JunctionSet(this);
            routes = new RouteSet(this);
            edges = new EdgeSet(this);
            sensors = new SensorSet(this);
            signals  = new SignalSet(this);
            outputs = new OutputSet(this);
        }

        /// <summary>
        /// Gets all blocks contained in this module.
        /// </summary>
        public BlockSet Blocks { get { return blocks; } }

        /// <summary>
        /// Control serialization of <see cref="Blocks"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBlocks() { return (blocks.Count > 0); }

        /// <summary>
        /// Gets all block groups contained in this module.
        /// </summary>
        public BlockGroupSet BlockGroups { get { return blockGroups; } }

        /// <summary>
        /// Control serialization of <see cref="BlockGroups"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBlockGroups() { return (blockGroups.Count > 0); }

        /// <summary>
        /// Gets all edges of this module.
        /// </summary>
        public EdgeSet Edges { get { return edges; } }

        /// <summary>
        /// Control serialization of <see cref="Edges"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeEdges() { return (edges.Count > 0); }

        /// <summary>
        /// Gets all junctions contained in this module.
        /// </summary>
        public JunctionSet Junctions { get { return junctions; } }

        /// <summary>
        /// Control serialization of <see cref="Junctions"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeJunctions() { return (junctions.Count > 0); }

        /// <summary>
        /// Gets all sensors contained in this module.
        /// </summary>
        public SensorSet Sensors { get { return sensors; } }

        /// <summary>
        /// Control serialization of <see cref="Sensors"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSensors() { return (sensors.Count > 0); }

        /// <summary>
        /// Gets all signals contained in this module.
        /// </summary>
        public SignalSet Signals { get { return signals; } }

        /// <summary>
        /// Control serialization of <see cref="Signals"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSignals() { return (signals.Count > 0); }

        /// <summary>
        /// Gets all outputs contained in this module.
        /// </summary>
        public OutputSet Outputs { get { return outputs; } }

        /// <summary>
        /// Control serialization of <see cref="Outputs"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOutputs() { return (outputs.Count > 0); }

        /// <summary>
        /// Gets all routes contained in this module.
        /// </summary>
        public RouteSet Routes { get { return routes; } }

        /// <summary>
        /// Control serialization of <see cref="Routes"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRoutes() { return (routes.Count > 0); }

        /// <summary>
        /// Gets/sets the background image of this module.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        [XmlIgnore]
        public Stream BackgroundImage
        {
            get
            {
                var pkg = Package;
                return (pkg != null) ? pkg.GetGenericPart(this, BackgroundImageId) : null;
            }
            set
            {
                backgroundImageSize = null;
                var pkg = Package;
                if (pkg == null)
                {
                    throw new ArgumentException("Cannot set background-image when not part of a package");
                }
                if (value == null)
                {
                    pkg.RemoveGenericPart(this, BackgroundImageId);
                }
                else
                {
                    pkg.SetGenericPart(this, BackgroundImageId, value);
                }
                OnModified();
            }
        }

        /// <summary>
        /// Horizontal size (in pixels) of this entity.
        /// </summary>
        public int Width
        {
            get 
            { 
                var entities = this.GetPositionedEntities().ToList();
                var minLeft = Math.Min(0, entities.Any() ? entities.Min(x => x.X) : 0);
                var maxRight = entities.Any() ? entities.Max(x => x.X + x.Width) : 20;
                return Math.Max(maxRight - minLeft, BackgroundImageSize.Width);
            }
        }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        public int Height
        {
            get
            {
                var entities = this.GetPositionedEntities().ToList();
                var minTop = Math.Min(0, entities.Any() ? entities.Min(x => x.Y) : 0);
                var maxBottom = entities.Any() ? entities.Max(x => x.Y + x.Height) : 20;
                return Math.Max(maxBottom - minTop, BackgroundImageSize.Height);
            }
        }

        /// <summary>
        /// Gets package relative folder for this type of entity.
        /// </summary>
        internal override string PackageFolder
        {
            get { return PackageFolders.Module; }
        }

        /// <summary>
        /// Gets all blocks contained in this module.
        /// </summary>
        IEntitySet2<IBlock> IModule.Blocks { get { return Blocks.Set; } }

        /// <summary>
        /// Gets all block groups contained in this module.
        /// </summary>
        IEntitySet2<IBlockGroup> IModule.BlockGroups { get { return BlockGroups.Set; } }

        /// <summary>
        /// Gets all edges of this module.
        /// </summary>
        IEntitySet2<IEdge> IModule.Edges { get { return Edges.Set; } }

        /// <summary>
        /// Gets all junctions contained in this module.
        /// </summary>
        IJunctionSet IModule.Junctions { get { return Junctions.Set; } }

        /// <summary>
        /// Gets all sensors contained in this module.
        /// </summary>
        ISensorSet IModule.Sensors { get { return Sensors.Set; } }

        /// <summary>
        /// Gets all signals contained in this module.
        /// </summary>
        ISignalSet IModule.Signals { get { return Signals.Set; } }

        /// <summary>
        /// Gets all outputs contained in this module.
        /// </summary>
        IOutputSet IModule.Outputs { get { return Outputs.Set; } }

        /// <summary>
        /// Gets all routes contained in this module.
        /// </summary>
        IEntitySet2<IRoute> IModule.Routes { get { return Routes.Set; } }

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
            blocks.Validate(validationRoot, results);
            junctions.Validate(validationRoot, results);
            routes.Validate(validationRoot, results);
            edges.Validate(validationRoot, results);
            sensors.Validate(validationRoot, results);
            signals.Validate(validationRoot, results);
            outputs.Validate(validationRoot, results);

            // Check for duplicate addresses
            if (validationRoot == this)
            {
                var addressEntities = this.GetAddressEntities().ToList();
                addressEntities.WarnForDuplicateAddresses(results);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            blocks.CollectUsageInfo(subject, results);
            junctions.CollectUsageInfo(subject, results);
            routes.CollectUsageInfo(subject, results);
            edges.CollectUsageInfo(subject, results);
            sensors.CollectUsageInfo(subject, results);
            signals.CollectUsageInfo(subject, results);
            outputs.CollectUsageInfo(subject, results);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            blocks.RemovedFromPackage(entity);
            junctions.RemovedFromPackage(entity);
            routes.RemovedFromPackage(entity);
            edges.RemovedFromPackage(entity);
            sensors.RemovedFromPackage(entity);
            signals.RemovedFromPackage(entity);
            outputs.RemovedFromPackage(entity);
        }

        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        ImportComparison IImportableEntity.CompareTo(IImportableEntity target)
        {
            return CompareTo((IPersistentEntity) target);
        }

        /// <summary>
        /// Import this entity into the given package.
        /// </summary>
        void IImportableEntity.Import(IPackage target)
        {
            ((Package)target).Import(this);
            target.Railway.Modules.Add(target.GetModule(Id));
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameModule; }
        }

        /// <summary>
        /// Upgrade to the latest format
        /// </summary>
        internal override void Upgrade()
        {
            base.Upgrade();
            routes.Upgrade();
        }

        /// <summary>
        /// Gets the size of the background image
        /// </summary>
        private Size BackgroundImageSize
        {
            get
            {
                if (!backgroundImageSize.HasValue)
                {
                    backgroundImageSize = Size.Empty;
                    var imageStream = BackgroundImage;
                    if (imageStream != null)
                    {
                        try
                        {
                            using (var image = Image.FromStream(imageStream))
                            {
                                backgroundImageSize = image.Size;
                            }

                        }
                        catch
                        {
                            /* ignore */
                        }
                    }
                }
                return backgroundImageSize.Value;
            }
        }
    }
}

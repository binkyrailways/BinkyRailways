using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for persistent entities.
    /// </summary>
    [XmlInclude(typeof(CommandStation))]
    [XmlInclude(typeof(Loc))]
    [XmlInclude(typeof(LocGroup))]
    [XmlInclude(typeof(LocPredicate))]
    [XmlInclude(typeof(Module))]
    [XmlInclude(typeof(Railway))]
    public abstract class PersistentEntity : Entity, IPersistentEntity
    {
        private bool dirty;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected PersistentEntity()
        {
        }

        /// <summary>
        /// Package from which this entity was loaded.
        /// Can be null if newly created.
        /// </summary>
        internal IPackage Package { get; set; }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected override Railway Root
        {
            get { return (Railway) Package.Railway; }
        }

        /// <summary>
        /// Last modification time (in UTC) of the entity.
        /// </summary>
        public long LastModified { get; set; }

        /// <summary>
        /// Gets last modification date in UTC time.
        /// </summary>
        DateTime IPersistentEntity.LastModifiedUtc
        {
            get { return DateTime.FromBinary(LastModified); }
        }

        /// <summary>
        /// Gets last modification date in local time.
        /// </summary>
        DateTime IPersistentEntity.LastModified
        {
            get { return DateTime.FromBinary(LastModified).ToLocalTime(); }
        }

        /// <summary>
        /// Should a modification update the <see cref="LastModified"/> property?
        /// </summary>
        internal bool UpdateLastModified { get; set; }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal override void OnModified()
        {
            dirty = true;
            if (UpdateLastModified)
            {
                LastModified = DateTime.UtcNow.ToBinary();
            }
            if (Package != null)
            {
                ((IModifiable)Package).OnModified();
            }
        }

        /// <summary>
        /// Has this entity been modified since it was last loaded from disk?
        /// </summary>
        internal bool IsModified { get { return dirty; } }

        /// <summary>
        /// Gets package relative folder for this type of entity.
        /// </summary>
        internal abstract string PackageFolder { get; }

        /// <summary>
        /// Save myself to the given stream.
        /// </summary>
        internal void Save(Stream stream, bool resetModified)
        {
            var settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var container = new PersistentEntityContainer { Entity = this };
                var serializer = new XmlSerializer(container.GetType());
                serializer.Serialize(writer, container);
            }
            if (resetModified)
            {
                dirty = false;
            }
        }

        /// <summary>
        /// Export myself to xml.
        /// </summary>
        internal XElement Export()
        {
            var stream = new MemoryStream();
            Save(stream, false);
            stream.Position = 0;
            return XElement.Load(stream);
        }

        /// <summary>
        /// Load an entity from the given stream
        /// </summary>
        internal static PersistentEntity Load<T>(Stream stream)
            where T : PersistentEntity
        {
            using (var reader = new StreamReader(stream, Encoding.Unicode))
            {
                var serializer = new XmlSerializer(typeof (PersistentEntityContainer));
                var container = (PersistentEntityContainer)serializer.Deserialize(reader);
                var result = (T)container.Entity;
                result.dirty = false;
                result.UpdateLastModified = true;
                return result;
            }
        }

        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        protected ImportComparison CompareTo(IPersistentEntity target)
        {
            if (target == null)
                return ImportComparison.TargetDoesNotExists;
            IPersistentEntity source = this;
            return (source.LastModifiedUtc >= target.LastModifiedUtc) ? 
                ImportComparison.SourceInNewer : ImportComparison.TargetIsNewer;
        }
    }
}

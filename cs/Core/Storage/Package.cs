using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;

namespace BinkyRailways.Core.Storage
{
    /// <summary>
    /// Single file containing zero or more persistent entities.
    /// </summary>
    public sealed partial class Package : IPackage, IModifiable
    {
        /// <summary>
        /// Default extension for package files.
        /// </summary>
        public const string DefaultExt = ".brw";

        private const string RailwayId = "_default";
        private const string ContentTypeZip = "application/zip";
        private const string ContentTypeSharedObject = "application/x-binky-object";

        private readonly Railway railway;
        private readonly Dictionary<Uri, byte[]> parts = new Dictionary<Uri, byte[]>();
        private readonly Dictionary<Uri, PersistentEntity> loadedEntities = new Dictionary<Uri, PersistentEntity>();
        private bool dirty;

        /// <summary>
        /// Default ctor
        /// </summary>
        private Package(System.IO.Packaging.Package package)
        {
            if (package != null)
            {
                LoadAll(package);
            }
            railway = ReadEntity<Railway>(PackageFolders.Railway, RailwayId);
            if (railway == null)
            {
                railway = new Railway();
                railway.Package = this;
                railway.Id = Entity.UniqueId();
                var uri = CreatePartUri(railway.PackageFolder, RailwayId);
                loadedEntities[uri] = railway;
            }
            dirty = false;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        private Package(Dictionary<Uri, byte[]> parts)
        {
            this.parts = parts;
            railway = ReadEntity<Railway>(PackageFolders.Railway, RailwayId);
            if (railway == null)
            {
                railway = new Railway();
                railway.Package = this;
                railway.Id = Entity.UniqueId();
                var uri = CreatePartUri(railway.PackageFolder, RailwayId);
                loadedEntities[uri] = railway;
            }
            dirty = false;
        }

        /// <summary>
        /// Gets the default railway contained in this package.
        /// </summary>
        public IRailway Railway
        {
            get { return railway; }
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public void Validate(ValidationResults results)
        {
            foreach (var entity in loadedEntities.Values)
            {
                entity.Validate(railway, results);
            }
        }

        /// <summary>
        /// Load a package from the given stream.
        /// </summary>
        public static IPackage Load(string path)
        {
            using (var package = System.IO.Packaging.Package.Open(path, FileMode.Open, FileAccess.Read))
            {
                return new Package(package);
            }
        }

        /// <summary>
        /// Create a new empty package
        /// </summary>
        public static IPackage Create()
        {
            return new Package((System.IO.Packaging.Package)null);
        }

        /// <summary>
        /// Save to disk.
        /// </summary>
        public void Save(string path)
        {
            // Save to temporary path first
            var tmpPath = Path.GetTempFileName();
            using (var package = System.IO.Packaging.Package.Open(tmpPath, FileMode.Create))
            {
                var sharedObjects = new Dictionary<string, string>();

                // Add all loaded entities
                foreach (var entry in loadedEntities)
                {
                    var memStream = new MemoryStream();
                    entry.Value.Save(memStream, true);
                    SavePartContent(package, entry.Key, memStream.ToArray(), sharedObjects);
                }
                // Now all everything that was not de-serialized
                foreach (var entry in parts)
                {
                    if (!loadedEntities.ContainsKey(entry.Key))
                    {
                        SavePartContent(package, entry.Key, entry.Value, sharedObjects);
                    }
                }
            }

            // Copy over actual path.
            File.Copy(tmpPath, path, true);

            // Remove tmp path
            File.Delete(tmpPath);

            dirty = false;
        }

        /// <summary>
        /// Gets the content of the given part.
        /// Shared objects are resolved here.
        /// </summary>
        private static void SavePartContent(System.IO.Packaging.Package package, Uri uri, byte[] content, Dictionary<string, string> sharedObjects)
        {
            // Decide how to store this content
            if (content.Length < 80)
            {
                // Store directly
                var contentPart = package.CreatePart(uri, ContentTypeZip, CompressionOption.Normal);
                using (var contentPartStream = contentPart.GetStream(FileMode.Create, FileAccess.Write))
                {
                    contentPartStream.Write(content, 0, content.Length);
                }
            }
            else
            {
                // Store as shared object
                var hash = CreateHash(content);
                var hashKey = Hex(hash);

                // Create content part that contains the hash of the shared object.
                var contentPart = package.CreatePart(uri, ContentTypeSharedObject, CompressionOption.NotCompressed);
                using (var contentPartStream = contentPart.GetStream(FileMode.Create, FileAccess.Write))
                {
                    contentPartStream.Write(hash, 0, hash.Length);
                }

                // Does the shared object exists?
                if (!sharedObjects.ContainsKey(hashKey))
                {
                    // Create shared object part
                    var sharedObjectUri = CreateSharedObjectPartUri(hashKey);
                    var objectPart = package.CreatePart(sharedObjectUri, ContentTypeZip, CompressionOption.Normal);
                    using (var objectPartStream = objectPart.GetStream(FileMode.Create, FileAccess.Write))
                    {
                        objectPartStream.Write(content, 0, content.Length);
                    }
                    sharedObjects[hashKey] = hashKey;
                }
            }
        }

        /// <summary>
        /// Has this package been changed since the last save?
        /// </summary>
        public bool IsDirty { get { return dirty; } }

        /// <summary>
        /// Add the given entity to this package
        /// </summary>
        private void Add(PersistentEntity entity)
        {
            entity.EnsureId();
            entity.Package = this;
            var uri = CreatePartUri(entity.PackageFolder, entity.Id);
            loadedEntities[uri] = entity;
            OnModified();
        }

        /// <summary>
        /// Remove the given entity from this package
        /// </summary>
        void IPackage.Remove(IPersistentEntity entity)
        {
            var uri = CreatePartUri(((PersistentEntity)entity).PackageFolder, entity.Id);
            loadedEntities.Remove(uri);
            parts.Remove(uri);
            ((PersistentEntity) entity).Package = null;
            OnModified();
            // Notify all listeners
            foreach (var iterator in loadedEntities.Values.Cast<IPackageListener>())
            {
                iterator.RemovedFromPackage(entity);
            }
        }

        /// <summary>
        /// Import the given entity from the given package
        /// </summary>
        internal void Import(PersistentEntity entity)
        {
            IPackage me = this;

            // Remove existing generic parts
            var ids = me.GetGenericPartIDs(entity).ToList();
            foreach (var id in ids)
            {
                me.RemoveGenericPart(entity, id);
            }

            // Copy generic parts
            var source = entity.Package;
            if (source != null)
            {
                ids = source.GetGenericPartIDs(entity).ToList();
                foreach (var id in ids)
                {
                    var stream = source.GetGenericPart(entity, id);
                    me.SetGenericPart(entity, id, stream);
                }
            }

            // Remove existing entity (if any)
            var uri = CreatePartUri(entity.PackageFolder, entity.Id);
            loadedEntities.Remove(uri);
            parts.Remove(uri);

            // Add entity
            var pEntity = entity;
            // Save entity so we have a byte stream
            var memStream = new MemoryStream();
            pEntity.Save(memStream, false);
            parts[uri] = memStream.ToArray();
        }

        /// <summary>
        /// Add a new LocoBuffer type command station.
        /// </summary>
        ILocoBufferCommandStation IPackage.AddNewLocoBufferCommandStation()
        {
            var item = new LocoBufferCommandStation();
            Add(item);
            return item;
        }

        /// <summary>
        /// Add a new DCC over RS232 type command station.
        /// </summary>
        IDccOverRs232CommandStation IPackage.AddNewDccOverRs232CommandStation()
        {
            var item = new DccOverRs232CommandStation();
            Add(item);
            return item;
        }

        /// <summary>
        /// Add a new Ecos command station.
        /// </summary>
        IEcosCommandStation IPackage.AddNewEcosCommandStation()
        {
            var item = new EcosCommandStation();
            Add(item);
            return item;
        }

        /// <summary>
        /// Add a new MQTT command station.
        /// </summary>
        IBinkyNetCommandStation IPackage.AddNewBinkyNetCommandStation()
        {
            var item = new BinkyNetCommandStation();
            Add(item);
            return item;
        }

        /// <summary>
        /// Add a new P50x type command station.
        /// </summary>
        IP50xCommandStation IPackage.AddNewP50xCommandStation()
        {
            var item = new P50xCommandStation();
            Add(item);
            return item;
        }

        /// <summary>
        /// Load a command station by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        ICommandStation IPackage.GetCommandStation(string id)
        {
            return ReadEntity<CommandStation>(PackageFolders.CommandStation, id);
        }

        /// <summary>
        /// Get all command stations
        /// </summary>
        IEnumerable<ICommandStation> IPackage.GetCommandStations()
        {
            return ReadEntities<CommandStation>(PackageFolders.CommandStation).Cast<ICommandStation>();
        }

        /// <summary>
        /// Add a new loc.
        /// </summary>
        ILoc IPackage.AddNewLoc()
        {
            var item = new Loc();
            Add(item);
            return item;
        }

        /// <summary>
        /// Load a loc by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        ILoc IPackage.GetLoc(string id)
        {
            return ReadEntity<Loc>(PackageFolders.Loc, id);
        }

        /// <summary>
        /// Get all locs
        /// </summary>
        IEnumerable<ILoc> IPackage.GetLocs()
        {
            return ReadEntities<Loc>(PackageFolders.Loc).Cast<ILoc>();
        }

        /// <summary>
        /// Add a new module.
        /// </summary>
        IModule IPackage.AddNewModule()
        {
            var item = new Module();
            Add(item);
            return item;
        }

        /// <summary>
        /// Load a module by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        IModule IPackage.GetModule(string id)
        {
            return ReadEntity<Module>(PackageFolders.Module, id);
        }

        /// <summary>
        /// Get all modules
        /// </summary>
        IEnumerable<IModule> IPackage.GetModules()
        {
            return ReadEntities<Module>(PackageFolders.Module).Cast<IModule>();
        }

        /// <summary>
        /// Gets the ID's of all generic parts that belong to the given entity.
        /// </summary>
        IEnumerable<string> IPackage.GetGenericPartIDs(IPersistentEntity entity)
        {
            var prefix = CreateGenericPartUri(entity, string.Empty).OriginalString;
            foreach (var uri in parts.Keys.Where(x => x.OriginalString.StartsWith(prefix)))
            {
                var id = uri.OriginalString.Substring(prefix.Length);
                yield return id;
            }            
        }

        /// <summary>
        /// Load a generic file part by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        Stream IPackage.GetGenericPart(IPersistentEntity entity, string id)
        {
            var uri = CreateGenericPartUri(entity, id);
            byte[] data;
            if (parts.TryGetValue(uri, out data))
            {
                return new MemoryStream(data);
            }
            return null;
        }

        /// <summary>
        /// Store a generic file part that belongs to the given entity by it's id.
        /// </summary>
        void IPackage.SetGenericPart(IPersistentEntity entity, string id, Stream source)
        {
            var uri = CreateGenericPartUri(entity, id);
            parts[uri] = ToArray(source);
        }

        /// <summary>
        /// Remove a generic file part that belongs to the given entity by it's id.
        /// </summary>
        void IPackage.RemoveGenericPart(IPersistentEntity entity, string id)
        {
            var uri = CreateGenericPartUri(entity, id);
            if (parts.Remove(uri))
            {
                OnModified();
            }
        }

        /// <summary>
        /// Load all parts
        /// </summary>
        private void LoadAll(System.IO.Packaging.Package package)
        {
            var sharedObjectCache = new Dictionary<string, byte[]>();
            foreach (var part in package.GetParts().Where(x => !IsSharedObject(x.Uri)))
            {
                // Load the part's content and store it.
                var bytes = GetPartContent(package, part, sharedObjectCache);
                parts[part.Uri] = bytes;
            }
        }

        /// <summary>
        /// Read an entity in a given folder.
        /// </summary>
        /// <returns>Null if not found</returns>
        private T ReadEntity<T>(string folder, string id)
            where T : PersistentEntity
        {
            var uri = CreatePartUri(folder, id);
            PersistentEntity result;
            if (loadedEntities.TryGetValue(uri, out result))
                return (T)result;

            byte[] data;
            if (!parts.TryGetValue(uri, out data))
                return null;

            try
            {
                result = PersistentEntity.Load<T>(new MemoryStream(data));
            }
            catch (Exception ex)
            {
                var xml = Encoding.Unicode.GetString(data);
                Version version;
                if (PersistentEntityContainer.TryGetVersion(xml, out version))
                {
                    if (version > GetType().Assembly.GetName().Version)
                    {
                        throw new ApplicationException(string.Format("This entity was created with version {0} which is a newer version: ", version));
                    }
                }
                throw new ApplicationException("Cannot parse: " + xml, ex);
            }
            result.Package = this;
            loadedEntities.Add(uri, result);
            result.Upgrade();
            return (T) result;
        }

        /// <summary>
        /// Get all command stations
        /// </summary>
        private IEnumerable<T> ReadEntities<T>(string folder)
            where T : PersistentEntity
        {
            var prefix = "/" + folder + "/";
            foreach (var uri in parts.Keys.Where(x => x.OriginalString.StartsWith(prefix)))
            {
                yield return ReadEntity<T>(folder, uri.OriginalString.Substring(prefix.Length));
            }
        }

        /// <summary>
        /// Create an uri for a part with given id in given folder.
        /// </summary>
        private static Uri CreatePartUri(string folder, string id)
        {
            return new Uri("/" + folder + "/" + id, UriKind.Relative);
        }

        /// <summary>
        /// Create an uri for a generic part with given id in given folder.
        /// </summary>
        private static Uri CreateGenericPartUri(IPersistentEntity entity, string id)
        {
            return new Uri("/" + PackageFolders.GenericParts + "/" + entity.Id + "/" + id, UriKind.Relative);
        }

        /// <summary>
        /// Create an uri for a shared object with a given hash.
        /// </summary>
        private static Uri CreateSharedObjectPartUri(string objectHash)
        {
            return new Uri("/" + PackageFolders.Objects + "/" + objectHash, UriKind.Relative);
        }

        /// <summary>
        /// Is the given Uri an Uri of a shared object?
        /// </summary>
        private static bool IsSharedObject(Uri uri)
        {
            return uri.OriginalString.StartsWith("/" + PackageFolders.Objects + "/");
        }

        /// <summary>
        /// Called when a property on this is modified
        /// </summary>
        public void OnModified()
        {
            dirty = true;
        }

        /// <summary>
        /// Make sure all entities are loaded.
        /// </summary>
        private void LoadAllEntities()
        {
            IPackage pkg = this;
            pkg.GetCommandStations().ToList();
            pkg.GetLocs().ToList();
            pkg.GetModules().ToList();
        }

        /// <summary>
        /// Gets the content of the given part.
        /// Shared objects are resolved here.
        /// </summary>
        private static byte[] GetPartContent(System.IO.Packaging.Package package, PackagePart part, Dictionary<string, byte[]> sharedObjectCache)
        {
            // Check type of part and load content according to this type.
            if (part.ContentType == ContentTypeSharedObject)
            {
                // Content is stored in a shared object
                var hash = Hex(ToArray(part.GetStream()));
                byte[] bytes;
                if (sharedObjectCache.TryGetValue(hash, out bytes))
                    return bytes;
                var objectPart = package.GetPart(CreateSharedObjectPartUri(hash));
                bytes = ToArray(objectPart.GetStream(FileMode.Open));
                sharedObjectCache[hash] = bytes;
                return bytes;
            }

            // Content is stored unshared
            return ToArray(part.GetStream(FileMode.Open));
        }

        /// <summary>
        /// Read a stream into a byte array.
        /// </summary>
        private static byte[] ToArray(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Create a shared object hash.
        /// </summary>
        private static byte[] CreateHash(byte[] data)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var hashBytes = sha1.ComputeHash(data);
            return hashBytes;
        }

        /// <summary>
        /// Create a HEX string from the given bytes.
        /// </summary>
        private static string Hex(byte[] bytes)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bytes[i]);
            }
            return sb.ToString();
        }
    }
}

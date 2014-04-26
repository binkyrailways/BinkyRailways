using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Connection between the edges of two modules.
    /// </summary>
    public class ModuleConnection : RailwayEntity, IModuleConnection
    {
        private string moduleAId;
        private string moduleBId;
        private readonly Property<EntityRef<Edge>> edgeA;
        private readonly Property<EntityRef<Edge>> edgeB;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleConnection()
        {
            edgeA = new Property<EntityRef<Edge>>(this, null);
            edgeB = new Property<EntityRef<Edge>>(this, null);
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        [XmlIgnore]
        public override string Description
        {
            get
            {
                IModuleConnection mc = this;
                var a = (mc.EdgeA != null) ? mc.EdgeA.GlobalDescription() : "?";
                var b = (mc.EdgeB != null) ? mc.EdgeB.GlobalDescription() : "?";

                if (string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    var tmp = b;
                    b = a;
                    a = tmp;
                }
                return a + " <-> " + b;
            }
            set { base.Description = value; }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription { get { return true; } }

        /// <summary>
        /// The first module in the connection
        /// </summary>
        [XmlIgnore]
        IModule IModuleConnection.ModuleA
        {
            get
            {
                IModuleConnection mc = this;
                return (mc.EdgeA != null) ? mc.EdgeA.Module : null;
            }
        }

        /// <summary>
        /// Edge of module A
        /// </summary>
        [XmlIgnore]
        IEdge IModuleConnection.EdgeA
        {
            get
            {
                if (edgeA.Value == null)
                    return null;
                Edge result;
                return edgeA.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (((IModuleConnection)this).EdgeA != value)
                {
                    var edge = value as Edge;
                    edgeA.Value = (edge != null) ? new EntityRef<Edge>(this, edge) : null;
                    moduleAId = (edge != null) ? edge.Module.Id : null;
                }
            }
        }

        /// <summary>
        /// The second module in the connection
        /// </summary>
        [XmlIgnore]
        IModule IModuleConnection.ModuleB 
        {
            get
            {
                IModuleConnection mc = this;
                return (mc.EdgeB != null) ? mc.EdgeB.Module : null;
            }
        }

        /// <summary>
        /// Edge of module B
        /// </summary>
        [XmlIgnore]
        IEdge IModuleConnection.EdgeB
        {
            get
            {
                if (edgeB.Value == null)
                    return null;
                Edge result;
                return edgeB.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (((IModuleConnection)this).EdgeB != value)
                {
                    var edge = value as Edge;
                    edgeB.Value = (edge != null) ? new EntityRef<Edge>(this, edge) : null;
                    moduleBId = (edge != null) ? edge.Module.Id : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the module A.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ModuleIdA
        {
            get
            {
                IModuleConnection mc = this;
                return (mc.ModuleA != null) ? mc.ModuleA.Id : null;
            }
            set { moduleAId = value; }
        }

        /// <summary>
        /// Gets the id of the module B.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ModuleIdB
        {
            get
            {
                IModuleConnection mc = this;
                return (mc.ModuleB != null) ? mc.ModuleB.Id : null;
            }
            set { moduleBId = value; }
        }

        /// <summary>
        /// Gets the id of the edge A.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string EdgeIdA
        {
            get { return edgeA.GetId(); }
            set { edgeA.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Edge>(this, value, LookupAEdge); }
        }

        /// <summary>
        /// Gets the id of the edge B.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string EdgeIdB
        {
            get { return edgeB.GetId(); }
            set { edgeB.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Edge>(this, value, LookupBEdge); }
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
            IModuleConnection mc = this;
            if (mc.ModuleA == null)
                results.Warn(this, Strings.WarnModuleANotSpecified);
            else if (mc.EdgeA == null)
                results.Warn(this, Strings.WarnEdgeANotSpecified);
            if (mc.ModuleB == null)
                results.Warn(this, Strings.WarnModuleBNotSpecified);
            else if (mc.EdgeB == null)
                results.Warn(this, Strings.WarnEdgeBNotSpecified);
        }

        /// <summary>
        /// Lookup a module by id. 
        /// </summary>
        private Module LookupModule(string id)
        {
            var moduleRef = Railway.Modules[id];
            if (moduleRef == null)
                return null;
            IModule module;
            return moduleRef.TryResolve(out module) ? (Module) module : null;
        }

        /// <summary>
        /// Lookup an edge by id. 
        /// </summary>
        private static Edge LookupEdge(string id, IModule module)
        {
            return (module != null) ? (Edge)module.Edges.FirstOrDefault(x => x.Id == id) : null;
        }

        /// <summary>
        /// Lookup edge A by id. 
        /// </summary>
        private Edge LookupAEdge(string id)
        {
            return LookupEdge(id, LookupModule(moduleAId));
        }

        /// <summary>
        /// Lookup edge B by id. 
        /// </summary>
        private Edge LookupBEdge(string id)
        {
            return LookupEdge(id, LookupModule(moduleBId));
        }

        /// <summary>
        /// Remove this connection is one of the modules is removed.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            IModuleConnection conn = this;
            if ((entity == conn.ModuleA) ||
                (entity == conn.ModuleB))
            {
                Railway.ModuleConnections.Remove(this);
            }
        }
    }
}

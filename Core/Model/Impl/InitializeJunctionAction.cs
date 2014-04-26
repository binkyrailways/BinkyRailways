using System.ComponentModel;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that involve a loc.
    /// </summary>
    public sealed class InitializeJunctionAction : ModuleAction, IInitializeJunctionAction
    {
        private readonly Property<EntityRef<Junction>> junction;

        /// <summary>
        /// Default ctor
        /// </summary>
        public InitializeJunctionAction()
        {
            junction = new Property<EntityRef<Junction>>(this, null);
        }

        /// <summary>
        /// Gets/Sets the junction involved in the action.
        /// </summary>
        [XmlIgnore]
        public Junction Junction
        {
            get
            {
                if (junction.Value == null)
                    return null;
                Junction result;
                return junction.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (Junction != value)
                {
                    junction.Value = (value != null) ? new EntityRef<Junction>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the junction.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string JunctionId
        {
            get { return junction.GetId(); }
            set { junction.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Junction>(this, value, LookupJunction); }
        }

        /// <summary>
        /// Gets/Sets the loc involved.
        /// </summary>
        IJunction IInitializeJunctionAction.Junction
        {
            get { return Junction; }
            set { Junction = (Junction)value; }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        public override string Description
        {
            get
            {
                var junc = Junction;
                return string.Format("Initialize {0}", (junc == null) ? "?" : junc.ToString());
            }
            set { /* Do nothing */ }
        }

        /// <summary>
        /// Lookup a junction by id.
        /// </summary>
        private Junction LookupJunction(string id)
        {
            var module = Module;
            return (module == null) ? null : module.Junctions[id];
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        protected override Action Clone()
        {
            return new InitializeJunctionAction { Junction = Junction };
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameInitializeJunctionAction; }
        }
    }
}

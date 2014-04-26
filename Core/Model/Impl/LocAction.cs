using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that involve a loc.
    /// </summary>
    public abstract class LocAction : Action, ILocAction
    {
        private readonly Property<EntityRef<Loc>> loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected LocAction()
        {
            loc = new Property<EntityRef<Loc>>(this, null);
        }

        /// <summary>
        /// Gets/Sets the loc involved in the action.
        /// </summary>
        [XmlIgnore]
        public Loc Loc
        {
            get
            {
                if (loc.Value == null)
                    return null;
                Loc result;
                return loc.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (Loc != value)
                {
                    loc.Value = (value != null) ? new EntityRef<Loc>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the loc.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string LocId
        {
            get { return loc.GetId(); }
            set { loc.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Loc>(this, value, LookupLoc); }
        }

        /// <summary>
        /// Gets/Sets the loc involved.
        /// </summary>
        ILoc ILocAction.Loc
        {
            get { return Loc; }
            set { Loc = (Loc)value; }
        }

        /// <summary>
        /// Lookup a loc by id.
        /// </summary>
        private Loc LookupLoc(string id)
        {
            ILoc loc;
            if (Root.TryResolveLoc(id, out loc))
                return (Loc) loc;
            return null;
        }
    }
}

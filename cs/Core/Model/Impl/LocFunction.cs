using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that are invoked upon a changing sensor value.
    /// </summary>
    [XmlType("LocFunctionEntity")]
    public class LocFunction : Entity, ILocFunction
    {
        private readonly Property<Model.LocFunction> function;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocFunction()
        {
            function = new Property<Model.LocFunction>(this, Model.LocFunction.Light);
        }

        /// <summary>
        /// The function involved in the action.
        /// </summary>
        public Model.LocFunction Function
        {
            get { return function.Value; }
            set { function.Value = value; }
        }

        /// <summary>
        /// Owner of this function.
        /// </summary>
        internal Loc Owner { get; set; }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected override Railway Root
        {
            get
            {
                var package = (Owner != null) ? Owner.Package : null;
                return (Railway) ((package != null) ? package.Railway : null);
            }
        }
    }
}

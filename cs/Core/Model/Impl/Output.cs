using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device that triggers some output on the railway.
    /// </summary>
    [XmlInclude(typeof(BinaryOutput))]
    [XmlInclude(typeof(Clock4StageOutput))]
    public abstract class Output : PositionedModuleEntity, IOutput
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected Output()
            : this(12, 12)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Output(int defaultWidth, int defaultHeight)
            : base(defaultWidth, defaultHeight)
        {
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}

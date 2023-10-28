namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of outputs contained in this module.
        /// </summary>
        public class OutputSet : ModuleEntitySet<Output, IOutput>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal OutputSet(Module module)
                : base(module, module)
            {
            }

            /// <summary>
            /// Implementation of IOutputSet
            /// </summary>
            public new IOutputSet Set
            {
                get { return (IOutputSet)base.Set; }
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new OutputSetImpl(this);
            }

            private sealed class OutputSetImpl : SetImpl, IOutputSet
            {
                /// <summary>
                /// Default ctor
                /// </summary>
                internal OutputSetImpl(OutputSet impl)
                    : base(impl)
                {
                }

                /// <summary>
                /// Add a new binary output
                /// </summary>
                public IBinaryOutput AddNewBinaryOutput()
                {
                    var item = new BinaryOutput();
                    Add(item);
                    return item;
                }

                /// <summary>
                /// Add a new 4-stage clock output
                /// </summary>
                public IClock4StageOutput AddNewClock4StageOutput()
                {
                    var item = new Clock4StageOutput();
                    Add(item);
                    return item;
                }
            }
        }
    }
}
